// 
//  PROJECT  :   KFlearning
//  FILENAME :   VscodeTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

#endregion

namespace KFlearning.Core.Services.Sequence
{
    public class VscodeTask : ITaskNode
    {
        public string TaskName => "Visual Studio Code";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var process = definition.ResolveService<IProcessManager>();
            var root = path.GetPath(PathKind.PathVscodeRoot);

            // find zip and extract
            progress.ReportMessage("Extracting Visual Studio Code...");
            var vscodeName = Environment.Is64BitOperatingSystem ? "VSCode-win32-x64*" : "VSCode-win32-ia32*";
            var vscodeZip = fileSystem.FindFile(definition.DataPath, vscodeName);
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(vscodeZip, root);
            }

            // create data directory
            fileSystem.CreateDirectory(path.Combine(root, "data"));
            fileSystem.CreateDirectory(path.Combine(root, @"data\user-data"));

            // install extensions
            progress.ReportMessage("Installing extensions...");
            var extensions = definition.VscodeExtensions;
            for (var i = 0; i < extensions.Count; i++)
            {
                var args = $"--install-extension \"{extensions[i]}\"";
                process.RunWait(path.GetPath(PathKind.CmdVscode), args);
                progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, extensions.Count));
            }

            // save settings
            progress.ReportMessage("Configuring Visual Studio Code...");
            var vscodeSettingsFile = path.Combine(root, @"data\user-data\settings.json");
            fileSystem.WriteFile(vscodeSettingsFile, Constants.VscodeConfig);
        }
    }
}