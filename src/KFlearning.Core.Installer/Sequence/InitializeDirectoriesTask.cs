// 
//  PROJECT  :   KFlearning
//  FILENAME :   InitializeDirectoriesTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.IO;
using System.Threading;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Installer.Sequence
{
    public class InitializeDirectoriesTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IPathManager _path;
        private readonly IFileSystemManager _fileSystem;

        public string TaskName => "Initialize Directories";

        public InitializeDirectoriesTask(IProgressBroker progress, IPathManager path, IFileSystemManager fileSystem)
        {
            _progress = progress;
            _path = path;
            _fileSystem = fileSystem;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            _progress.ReportNodeProgress(-1);
            if (definition.IsInstall)
            {
                
                Directory.CreateDirectory(_path.InstallRoot);
                Directory.CreateDirectory(_path.GetModuleInstallPath(ModuleKind.Ide));
                Directory.CreateDirectory(_path.GetModuleInstallPath(ModuleKind.Mingw));
                Directory.CreateDirectory(_path.GetModuleInstallPath(ModuleKind.Vscode));
                Directory.CreateDirectory(_path.GetModuleInstallPath(ModuleKind.ReposDirectory));
            }
            else
            {
                _fileSystem.DeleteDirectory(_path.InstallRoot, cancellation);
            }
        }
    }
}