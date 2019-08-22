// 
//  PROJECT  :   KFlearning
//  FILENAME :   KflearningTask.cs
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
    public class KflearningTask : ITaskNode
    {
        public string TaskName => "KFlearning IDE";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();

            // find zip and extract
            progress.ReportMessage("Extracting KFlearning...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "kflearning-ide-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(apacheZip, path.GetPath(PathKind.PathKflearningRoot));
            }

            // save content
            progress.ReportMessage("Creating default hosts...");
            var contentPath = path.Combine(PathKind.PathBase, @"etc\kflearning\index.html");
            fileSystem.WriteFile(contentPath, Constants.IndexPageHtml);

            contentPath = path.Combine(PathKind.PathBase, @"etc\kflearning\phpinfo.php");
            fileSystem.WriteFile(contentPath, Constants.PhpInfoPage);


            // add default site alias
            progress.ReportMessage("Creating HTTPD alias...");
            var indexPath = path.EnsureBackslashEnding(path.EnsureForwardSlash(
                path.Combine(PathKind.PathBase, @"etc\kflearning")));
            var defaultAliasPath = path.Combine(PathKind.PathBase, @"etc\apache\alias\0-default.conf");
            using (var alias = new TransformingConfigFile(defaultAliasPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "kflearning");
                alias.Transform("{ALIAS_PATH}", indexPath);
            }

            // add default site virtual host
            progress.ReportMessage("Creating HTTPD virtual host...");
            var defaultHostPath = path.Combine(PathKind.PathBase, @"etc\apache\sites-enabled\0-default.conf");
            using (var config = new TransformingConfigFile(defaultHostPath, Constants.DefaultVirtualHost))
            {
                config.Transform("{KFLEARNING_DIR_ROOT}", indexPath);
            }

            // create shortcut
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Creating desktop shortcut...");
            var exePath = path.Combine(PathKind.PathKflearningRoot, "KFlearning.IDE.exe");
            fileSystem.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);
        }
    }
}