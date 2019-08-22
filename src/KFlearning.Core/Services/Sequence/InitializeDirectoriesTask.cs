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

using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

#endregion

namespace KFlearning.Core.Services.Sequence
{
    public class InitializeDirectoriesTask : ITaskNode
    {
        private readonly bool _install;

        public string TaskName => "Initialize Directories";

        public InitializeDirectoriesTask(bool install)
        {
            _install = install;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var baseDir = path.GetPath(PathKind.PathBase);

            progress.ReportNodeProgress(-1);
            if (_install)
            {
                fileSystem.CreateDirectory(baseDir);
                fileSystem.CreateDirectory(path.Combine(baseDir, "bin"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\httpd"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\mariadb"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\mingw"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\php"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\composer"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"bin\vscode"));

                fileSystem.CreateDirectory(path.Combine(baseDir, "etc"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\apache"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\apache\alias"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\apache\sites-enabled"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\kflearning"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\phpMyAdmin"));
                fileSystem.CreateDirectory(path.Combine(baseDir, @"etc\templates"));

                fileSystem.CreateDirectory(path.Combine(baseDir, "ide"));
                fileSystem.CreateDirectory(path.Combine(baseDir, "repos"));
            }
            else
            {
                fileSystem.DeleteDirectory(path.Combine(baseDir, "bin"), cancellation);
                fileSystem.DeleteDirectory(path.Combine(baseDir, "etc"), cancellation);
                fileSystem.DeleteDirectory(path.Combine(baseDir, "ide"), cancellation);
            }
        }
    }
}