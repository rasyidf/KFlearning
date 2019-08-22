// 
//  PROJECT  :   KFlearning
//  FILENAME :   ComposerTask.cs
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
    public class ComposerTask : ITaskNode
    {
        public string TaskName => "Composer Installation";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var path = definition.ResolveService<IPathManager>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();

            // install composer
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Installing composer...");
            var composerFile = path.Combine(definition.DataPath, "composer.phar");
            var composerInstallPath = path.Combine(PathKind.PathComposerRoot, "composer.phar");
            fileSystem.CopyFile(composerFile, composerInstallPath);

            // create batch execution file
            var composerBatchPath = path.Combine(PathKind.PathComposerRoot, "composer.bat");
            fileSystem.WriteFile(composerBatchPath, Constants.ComposerBatch);
        }
    }
}