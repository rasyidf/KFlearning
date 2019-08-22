// 
//  PROJECT  :   KFlearning
//  FILENAME :   PhpTask.cs
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
    public class PhpTask : ITaskNode
    {
        public string TaskName => "PHP Interpreter";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathPhpRoot);

            // find zip and extract
            progress.ReportMessage("Extracting PHP...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "php-*.zip");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(apacheZip, root);
            }

            // install xdebug
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Installing Xdebug to PHP...");
            var xdebugFile = fileSystem.FindFile(definition.DataPath, "php_xdebug*");
            var xdebugInstallPath = path.Combine(root, "ext", path.GetFileName(xdebugFile) ?? "");
            fileSystem.CopyFile(xdebugFile, xdebugInstallPath);

            // save settings (php.ini)
            progress.ReportMessage("Configuring PHP...");
            using (var config = new TransformingConfigFile(path.Combine(root, "php.ini"), Constants.PhpConfig))
            {
                config.Transform("{XDEBUG_PATH}", path.EnsureForwardSlash(xdebugInstallPath));
            }
        }
    }
}