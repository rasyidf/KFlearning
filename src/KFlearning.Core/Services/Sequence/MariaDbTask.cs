using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class MariaDbTask : ITaskNode
    {
        public string TaskName => "MariaDB";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMariaDbRoot);

            // find zip and extract
            progress.ReportMessage("Extracting MariaDB...");
            var mariaDbPath = fileSystem.FindFile(definition.DataPath, "mariadb-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(mariaDbPath, root);
            }

            // move directory (un-nesting)
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Un-nesting directory...");
            var rootNested = fileSystem.FindDirectory(root, "mariadb-*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);

            //configure
            progress.ReportMessage("Configuring MariaDB...");
            var configFile = path.Combine(root, "my.ini");
            using (var config = new TransformingConfigFile(configFile, Constants.MariaDbConfig))
            {
                var rootDirBackslash = path.EnsureBackslashEnding(root);
                config.Transform("{MARIADB_INSTALL_ROOT}", rootDirBackslash);
            }
        }
    }
}
