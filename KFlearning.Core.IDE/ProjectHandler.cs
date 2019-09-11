// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectHandler.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IDE.Data;
using KFlearning.Core.IO;
using KFlearning.Core.Resources;
using KFlearning.Core.Services;
using Newtonsoft.Json;

#endregion

namespace KFlearning.Core.IDE
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly IVscode _vscode;
        private readonly IPathService _pathService;
        private readonly IProcessManager _processManager;
        private readonly IFileSystemManager _fileSystem;

        public ProjectHandler(IVscode vscode, IFileSystemManager fileSystem, IPathService pathService, IProcessManager processManager)
        {
            _vscode = vscode;
            _fileSystem = fileSystem;
            _pathService = pathService;
            _processManager = processManager;
        }

        public void Launch(Project project)
        {
            _vscode.OpenFolder(project.Path);
        }

        public void CreateLink(Project project)
        {
            var linkPath = Path.Combine(_pathService.LaragonWebRoot, project.Title);
            _fileSystem.CreateDirectoryLink(linkPath, project.Path);
        }

        public void RemoveLink(Project project)
        {
            var linkPath = Path.Combine(_pathService.LaragonWebRoot, project.Title);
            _fileSystem.RemoveDirectoryLink(linkPath);
        }

        public bool LinkExists(Project project)
        {
            var linkPath = Path.Combine(_pathService.LaragonWebRoot, project.Title);
            return _fileSystem.DirectoryLinkExists(linkPath);
        }

        public bool CanModifyLink()
        {
            return _processManager.IsProcessElevated();
        }

        public void SaveMetadata(Project project)
        {
            var path = Path.Combine(project.Path, Constants.MetadataFileName);
            using (var streamWriter = new StreamWriter(path))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                serializer.Serialize(streamWriter, project);
            }
        }

        public void InitializeCpp(Project project)
        {
            var zipPath = _pathService.GetTemplateZipPath(TemplateZip.Cpp);
            using (var zip = new ZipFile(zipPath))
            {
                zip.ExtractAll(project.Path);
            }
            File.Delete(zipPath);
        }
    }
}