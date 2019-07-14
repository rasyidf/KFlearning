using System.IO;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class MariaDbTask : ITaskNode
    {
        public string TaskName => "MariaDB";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMariaDbRoot);

            // find zip and extract
            var mariaDbPath = path.FindFile(definition.DataPath, "mariadb*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(mariaDbPath, root);

            // move directory (un-nesting)
            var rootNested = Directory.GetDirectories(root, "mariadb*",SearchOption.TopDirectoryOnly).First();
            path.RecursiveMoveDirectory(rootNested, root, cancellation);

            //configure
            var configFile = Path.Combine(root, "my.ini");
            using (var config = new TransformingConfigFile(configFile, Constants.MariaDbConfig))
            {
                var rootDirBackslash = path.EnsureBackslashEnding(root);
                config.Transform("{MARIADB_INSTALL_ROOT}", rootDirBackslash);
            }
        }
    }
}
