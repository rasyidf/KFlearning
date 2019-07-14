﻿// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using LiteDB;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public class ProjectManager : IProjectManager
    {
        private readonly IWebServer _webServer;
        private readonly IDatabaseContext _database;
        private readonly IPathManager _pathManager;
        private readonly IVscode _vscode;

        public ProjectManager(IWebServer webServer, IPathManager pathManager, IDatabaseContext database, IVscode vscode)
        {
            _webServer = webServer;
            _pathManager = pathManager;
            _database = database;
            _vscode = vscode;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _database.Projects.FindAll();
        }

        public bool Exists(string title)
        {
            var path = GetPathForProject(title);
            return Directory.Exists(path);
        }

        public void Create(ProjectType type, string title)
        {
            var project = new Project
            {
                Title = title,
                Type = type,
                Path = GetPathForProject(title),
                DomainName = type == ProjectType.Web ? _webServer.CreateDomainName(title) : "",
            };
            Directory.CreateDirectory(project.Path);

            if (type == ProjectType.Web)
            {
                _webServer.CreateAlias(project.DomainName, project.Path);
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
                _webServer.RemoveAlias(project.DomainName);
            }

            _database.Projects.Delete(project.ProjectId);
        }

        public bool CheckImportZip(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                return zip.FindEntry(Constants.MetadataFileName, true) != -1;
            }
        }

        public void Import(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                // extract the files
                var extractPath = GetPathForProject(zip.ZipFileComment);
                foreach (ZipEntry entry in zip)
                {
                    if (!entry.IsFile) continue;		
                    
                    string entryFileName = entry.Name;
                    byte[] buffer = new byte[4096];
                    
                    string fullZipToPath = Path.Combine(extractPath, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName?.Length > 0) Directory.CreateDirectory(directoryName);

                    using (FileStream streamWriter = File.Create(fullZipToPath)) 
                    {
                        Stream zipStream = zip.GetInputStream(entry);
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }

                // save metadata to db
                var metadataFile = Path.Combine(extractPath, Constants.MetadataFileName);
                var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(metadataFile));
                project.Path = extractPath; // save new project path

                if (project.Type == ProjectType.Web)
                {
                    _webServer.CreateAlias(project.DomainName, project.Path);
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
                    zip.Add(dire, CompressionMethod.Deflated);
                }

                zip.SetComment(project.Title);
                zip.CommitUpdate();
            }
        }

        public void Purge()
        {
            var dirs = Directory.EnumerateDirectories(_pathManager.GetPath(PathKind.PathReposRoot), "*",
                SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                Directory.Delete(dir, true);
            }

            _database.Projects.Delete(Query.All());
        }

        public string GetPathForProject(string title)
        {
            return Path.Combine(_pathManager.GetPath(PathKind.PathReposRoot),
                _pathManager.StripInvalidFileName(title).ToLowerInvariant());
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

        private void ExtractTemplate(Project project)
        {
            string path;
            switch (project.Type)
            {
                case ProjectType.Web:
                    path = _pathManager.GetPath(PathKind.TemplateWeb);
                    break;
                case ProjectType.Cpp:
                    path = _pathManager.GetPath(PathKind.TemplateCpp);
                    break;
                case ProjectType.Python:
                    path = _pathManager.GetPath(PathKind.TemplatePython);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(project.Type), project.Type, null);
            }

            using (var zip = new ZipFile(path))
            {
                foreach (ZipEntry entry in zip)
                {
                    if (!entry.IsFile) continue;		
                    
                    string entryFileName = entry.Name;
                    byte[] buffer = new byte[4096];
                    
                    string fullZipToPath = Path.Combine(project.Path, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName?.Length > 0) Directory.CreateDirectory(directoryName);

                    using (FileStream streamWriter = File.Create(fullZipToPath)) 
                    {
                        Stream zipStream = zip.GetInputStream(entry);
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
        }

        #endregion
    }
}