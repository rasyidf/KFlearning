using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class PhpMyAdminTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "phpMyAdmin";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.Combine(PathKind.PathBase, @"etc\phpmyadmin");

            // find zip and extract
            _progress.ReportMessage("Extracting phpMyAdmin...");
            var mariaDbPath = fileSystem.FindFile(definition.DataPath, "phpMyAdmin-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(mariaDbPath, root);
            extractor.StatusChanged -= Extractor_StatusChanged;

            // move directory (un-nesting)
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Un-nesting directory...");
            var rootNested = fileSystem.FindDirectory(root, "phpMyAdmin-*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);
            
            // add phpmyadmin to alias
            _progress.ReportMessage("Configuring HTTPD alias...");
            var configPath = path.Combine(PathKind.PathBase, @"etc\apache\alias\phpmyadmin.conf");
            using (var alias = new TransformingConfigFile(configPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "phpmyadmin");
                alias.Transform("{ALIAS_PATH}",
                    path.EnsureBackslashEnding(path.EnsureForwardSlash(root)));
            }
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
