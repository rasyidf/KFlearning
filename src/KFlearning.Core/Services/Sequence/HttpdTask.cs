using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class HttpdTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "Apache HTTPD";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathApacheRoot);

            // find zip and extract
            _progress.ReportMessage("Extracting httpd...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "httpd-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(apacheZip, root);
            extractor.StatusChanged -= Extractor_StatusChanged;

            // move directory (un-nesting)
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Un-nesting httpd...");
            var rootNested = fileSystem.FindDirectory(root, "Apache*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);

            // config apache
            _progress.ReportMessage("Configuring httpd...");
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

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
