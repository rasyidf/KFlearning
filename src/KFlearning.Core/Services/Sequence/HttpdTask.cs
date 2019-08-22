// 
//  PROJECT  :   KFlearning
//  FILENAME :   HttpdTask.cs
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
    public class HttpdTask : ITaskNode
    {
        public string TaskName => "Apache HTTPD";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathApacheRoot);

            // find zip and extract
            progress.ReportMessage("Extracting httpd...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "httpd-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(apacheZip, root);
            }

            // move directory (un-nesting)
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Un-nesting httpd...");
            var rootNested = fileSystem.FindDirectory(root, "Apache*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);

            // config apache
            progress.ReportMessage("Configuring httpd...");
            var configFile = path.Combine(root, @"conf\httpd.conf");
            using (var config = new TransformingConfigFile(configFile, Constants.HttpdConfig))
            {
                config.Transform("{HTTPD_ROOT}", path.EnsureForwardSlash(root));
                config.Transform("{DOCUMENT_ROOT}",
                    path.EnsureForwardSlash(path.GetPath(PathKind.PathReposRoot)));
                config.Transform("{ALIAS_PATH}",
                    path.EnsureForwardSlash(path.GetPath(PathKind.PathSitesAliasRoot)));
                config.Transform("{SITES_PATH}",
                    path.EnsureForwardSlash(path.GetPath(PathKind.PathVirtualHostRoot)));
                config.Transform("{PHP_PATH}",
                    path.EnsureForwardSlash(path.GetPath(PathKind.PathPhpRoot)));
                var phpModule = path.EnsureForwardSlash(path.Combine(PathKind.PathPhpRoot, "php7apache2_4.dll"));
                config.Transform("{PHP_MODULE_PATH}", phpModule);
            }
        }
    }
}