// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using LiteDB;
using Newtonsoft.Json;

#endregion

namespace KFlearning.Core.Services
{
    public class ProjectManager : IProjectManager
    {
        private readonly IDatabaseContext _database;
        private readonly IProjectHandler _projectHandler;
        private readonly IFileSystemManager _fileSystem;
        private readonly IPathManager _pathManager;

        public ProjectManager(IPathManager pathManager, IDatabaseContext database, IProjectHandler projectHandler,
            IFileSystemManager fileSystem)
        {
            _pathManager = pathManager;
            _database = database;
            _fileSystem = fileSystem;
            _projectHandler = projectHandler;
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

        public void Create(string title)
        {
            var path = GetPathForProject(title);
            var project = new Project
            {
                Title = title,
                Path = path
            };

            Directory.CreateDirectory(path);
            _database.Projects.Insert(project);
            _projectHandler.SaveMetadata(project);
        }

        public void Delete(Project project)
        {
            if (_projectHandler.LinkExists(project))
            {
                _projectHandler.RemoveLink(project);
            }

            _fileSystem.DeleteDirectory(project.Path, CancellationToken.None);
            _database.Projects.Delete(project.ProjectId);
        }

        public bool CheckImportZip(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                return zip.Cast<ZipEntry>().Any(x => x.Name.Contains(Constants.MetadataFileName));
            }
        }

        public void Import(string zipFile)
        {
            using (var zip = new ZipFile(zipFile))
            {
                // extract the files
                var extractPath = GetPathForProject(zip.ZipFileComment);
                zip.ExtractAll(extractPath);

                // save metadata to db
                var metadataFile = Path.Combine(extractPath, Constants.MetadataFileName);
                var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(metadataFile));

                // save new project path
                project.Path = extractPath;

                _database.Projects.Insert(project);
                _projectHandler.SaveMetadata(project);
            }
        }

        public void Export(Project project, string savePath)
        {
            using (var zip = ZipFile.Create(savePath))
            {
                var files = Directory.EnumerateFiles(project.Path, "*", SearchOption.AllDirectories);
                zip.NameTransform = new ZipNameTransform(project.Path);

                zip.BeginUpdate();
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
    }
}