using System.IO;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class HttpdTask : ITaskNode
    {
        public string TaskName => "Apache HTTPD";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathApacheRoot);

            // find zip and extract
            var apacheZip = path.FindFile(definition.DataPath, "httpd*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(apacheZip, root);

            // move directory (un-nesting)
            var rootNested = Directory.EnumerateDirectories(root, "Apache*", SearchOption.TopDirectoryOnly).First();
            path.RecursiveMoveDirectory(rootNested, root, cancellation);

            // config apache
            var configFile = Path.Combine(root, @"conf\httpd.conf");
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
                var phpModule = path.EnsureForwardSlash(Path.Combine(path.GetPath(PathKind.PathPhpRoot), "php7apache2_4.dll"));
                config.Transform("{PHP_MODULE_PATH}", phpModule);
            }
        }
    }
}
