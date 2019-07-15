// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.DAL;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Handlers;
using LiteDB;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public class ProjectManager : IProjectManager
    {
        private readonly IDatabaseContext _database;
        private readonly IPathManager _pathManager;
        private readonly IVscode _vscode;
        private readonly Dictionary<ProjectType, IProjectHandler> _projectHandlers;

        public ProjectManager(IPathManager pathManager, IDatabaseContext database, IVscode vscode,
            CppProjectHandler cppHandler, WebProjectHandler webHandler, PythonProjectHandler pythonHandler)
        {
            _pathManager = pathManager;
            _database = database;
            _vscode = vscode;

            _projectHandlers = new Dictionary<ProjectType, IProjectHandler>
            {
                {ProjectType.Cpp, cppHandler},
                {ProjectType.Web, webHandler},
                {ProjectType.Python, pythonHandler}
            };
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
            var path = GetPathForProject(title);
            var handler = _projectHandlers[type];
            var project = handler.Create(title, path);

            _database.Projects.Insert(project);
        }

        public void Launch(Project project)
        {
            _vscode.OpenFolder(project.Path);
        }

        public void Delete(Project project)
        {
            var handler = _projectHandlers[project.Type];
            handler.Destroy(project);
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
                zip.ExtractAll(extractPath);

                // save metadata to db
                var metadataFile = Path.Combine(extractPath, Constants.MetadataFileName);
                var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(metadataFile));

                // save new project path
                project.Path = extractPath;

                // initialize project
                var handler = _projectHandlers[project.Type];
                handler.Import(project);

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
    }
}