using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class PhpMyAdminTask : ITaskNode
    {
        public string TaskName => "phpMyAdmin";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.Combine(PathKind.PathBase, @"etc\phpmyadmin");

            // find zip and extract
            progress.ReportMessage("Extracting phpMyAdmin...");
            var mariaDbPath = fileSystem.FindFile(definition.DataPath, "phpMyAdmin-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(mariaDbPath, root);
            }

            // move directory (un-nesting)
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Un-nesting directory...");
            var rootNested = fileSystem.FindDirectory(root, "phpMyAdmin-*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);

            // add phpmyadmin to alias
            progress.ReportMessage("Configuring HTTPD alias...");
            var configPath = path.Combine(PathKind.PathBase, @"etc\apache\alias\phpmyadmin.conf");
            using (var alias = new TransformingConfigFile(configPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "phpmyadmin");
                alias.Transform("{ALIAS_PATH}",
                    path.EnsureBackslashEnding(path.EnsureForwardSlash(root)));
            }
        }
    }
}
