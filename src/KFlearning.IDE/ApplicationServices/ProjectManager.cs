// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using KFlearning.Core;
using KFlearning.Core.Entities;
using KFlearning.Core.Hosts;
using KFlearning.Core.IO;
using KFlearning.IDE.Models;
using LiteDB;
using Newtonsoft.Json;

namespace KFlearning.IDE.ApplicationServices
{
    public class ProjectManager : IProjectManager
    {
        private readonly IApacheServer _apache;
        private readonly IDatabaseContext _database;
        private readonly IHostsFile _hosts;
        private readonly IPathManager _pathManager;
        private readonly IVscode _vscode;

        public ProjectManager(IApacheServer apache, IHostsFile hosts, IPathManager pathManager,
            IDatabaseContext database, IVscode vscode)
        {
            _apache = apache;
            _hosts = hosts;
            _pathManager = pathManager;
            _database = database;
            _vscode = vscode;
        }

        public IEnumerable<ProjectItem> GetProjects()
        {
            var projects = _database.Projects.FindAll();
            return projects.Select(x => new ProjectItem(x));
        }

        public void Create(ProjectType type, string title)
        {
            var project = new Project
            {
                Title = title,
                Type = type,
                Path = GetPathForProject(title),
                DomainName = type == ProjectType.Web ? CreateDomainName(title) : "",
            };
            Directory.CreateDirectory(project.Path);

            if (type == ProjectType.Web)
            {
                _apache.CreateAlias(project.DomainName, project.Path);
                _hosts.AddEntry(project.DomainName);
            }

            ExtractTemplate(project);
            SaveMetadata(project);

            _database.Projects.Insert(project);
        }

        public void Launch(Project project)
        {
            _vscode.OpenFolder(project.Path);
        }

        public void Delete(Project project)
        {
            Directory.Delete(project.Path, true);
            if (project.Type == ProjectType.Web)
            {
                _apache.RemoveAlias(project.DomainName);
                _hosts.RemoveEntry(project.DomainName);
            }

            _database.Projects.Delete(project.ProjectId);
        }

        public void Import(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                if (!zip.EntryFileNames.Contains(Constants.MetadataFileName))
                    throw new InvalidOperationException("No metadata file exists on import ZIP file.");

                // extract the files
                var extractPath = GetPathForProject(zip.Comment);
                zip.ExtractAll(extractPath);

                // save metadata to db
                var metadataFile = Path.Combine(extractPath, Constants.MetadataFileName);
                var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(metadataFile));
                project.Path = extractPath; // save new project path

                if (project.Type != ProjectType.Web)
                {
                    _apache.CreateAlias(project.DomainName, project.Path);
                    _hosts.AddEntry(project.DomainName);
                }

                _database.Projects.Insert(project);
            }
        }

        public void Export(Project project, string savePath)
        {
            using (var zip = new ZipFile(savePath))
            {
                var files = Directory.EnumerateFiles(project.Path, "*", SearchOption.AllDirectories);
                foreach (string dire in files)
                {
                    zip.AddFile(dire);
                }

                zip.Comment = project.Title;
                zip.Save();
            }
        }

        public void Purge()
        {
            var dirs = Directory.EnumerateDirectories(_pathManager.GetPath(PathKind.ReposRoot), "*",
                SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                Directory.Delete(dir, true);
            }

            _database.Projects.Delete(Query.All());
        }

        public string GetPathForProject(string title)
        {
            return Path.Combine(_pathManager.GetPath(PathKind.ReposRoot), StripDirectoryPath(title));
        }

        #region Private Methods

        private void SaveMetadata(Project project)
        {
            var path = Path.Combine(project.Path, Constants.MetadataFileName);
            using (var streamWriter = new StreamWriter(path))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(streamWriter, project);
            }
        }

        private string StripDirectoryPath(string title)
        {
            return title.ToLowerInvariant().Replace(" ", "_");
        }

        private string CreateDomainName(string title)
        {
            return $"{StripDirectoryPath(title)}.{Constants.DomainName}";
        }

        private void ExtractTemplate(Project project)
        {
            string path;
            switch (project.Type)
            {
                case ProjectType.Web:
                    path = _pathManager.GetPath(TemplateFile.Web);
                    break;
                case ProjectType.Cpp:
                    path = _pathManager.GetPath(TemplateFile.Cpp);
                    break;
                case ProjectType.Python:
                    path = _pathManager.GetPath(TemplateFile.Python);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(project.Type), project.Type, null);
            }

            using (var zip = new ZipFile(path))
            {
                zip.ExtractAll(project.Path, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        #endregion
    }
}