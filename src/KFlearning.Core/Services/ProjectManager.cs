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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;
using Newtonsoft.Json;

#endregion

namespace KFlearning.Core.Services
{
    public interface IProjectManager
    {
        List<string> Templates { get; }
        
        string GetPathForProject(string basePath, string title);

        bool IsValidProject(string path);

        Project Load(string path);

        Project Create(string title, string template, string path);
        void Launch(Project project);

        
        void SaveMetadata(Project project);
    }

    public class ProjectManager : IProjectManager
    {      
        private readonly IVscode _vscode;
        private readonly IProcessManager _process;
        private readonly IPathManager _path;
        private readonly IFileSystemManager _fileSystem;

        private readonly Dictionary<string, string> _projectTemplates;
        private readonly string _defaultProjectPath;

        public List<string> Templates => _projectTemplates.Keys.ToList();

        public ProjectManager(IVscode vscode, IFileSystemManager fileSystem, IProcessManager process, IPathManager path)
        {
            _vscode = vscode;
            _fileSystem = fileSystem;
            _process = process;
            _path = path;

            _projectTemplates = LoadTemplates();
            _defaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "KFlearning");
        }

        public bool IsValidProject(string path)
        {
            return File.Exists(Path.Combine(path, CoreResources.MetadataFileName));
        }

        public Project Load(string path)
        {
            var content = File.ReadAllText(Path.Combine(path, CoreResources.MetadataFileName));
            var project = JsonConvert.DeserializeObject<Project>(content);
            project.Path = path;

            return project;
        }

        public Project Create(string title, string template, string path)
        {
            var project = new Project
            {
                Title = title,
                Template = template,
                Path = path,
            };

            Directory.CreateDirectory(path);
            SaveMetadata(project);

            if (!_projectTemplates.TryGetValue(project.Template, out string zipPath)) return project;
            using (var zip = new ZipFile(zipPath))
            {
                zip.ExtractAll(project.Path);
            }

            return project;
        }

        public string GetPathForProject(string basePath, string title)
        {
            return Path.Combine(basePath ?? _defaultProjectPath, _path.StripInvalidFileName(title).ToLowerInvariant());
        }

        public void Launch(Project project)
        {
            _vscode.OpenFolder(project.Path);
        }

        public void SaveMetadata(Project project)
        {
            var path = Path.Combine(project.Path, CoreResources.MetadataFileName);
            using (var streamWriter = new StreamWriter(path))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                serializer.Serialize(streamWriter, project);
            }
        }

        private Dictionary<string, string> LoadTemplates()
        {
            var content = File.ReadAllText(_path.GetPath(PathKind.ProjectTemplatesJson));
            var lst = new[] {new {name = "", file = ""}}.ToList();
            lst = JsonConvert.DeserializeAnonymousType(content, lst);

            return lst.ToDictionary(x => x.name, y => Path.Combine(_path.GetPath(PathKind.InstallRoot), y.file));
        }
    }
}