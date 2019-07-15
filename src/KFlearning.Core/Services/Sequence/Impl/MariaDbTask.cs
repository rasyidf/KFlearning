using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class MariaDbTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "MariaDB";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMariaDbRoot);

            // find zip and extract
            _progress.ReportMessage("Extracting MariaDB...");
            var mariaDbPath = fileSystem.FindFile(definition.DataPath, "mariadb-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(mariaDbPath, root);
            extractor.StatusChanged -= Extractor_StatusChanged;

            // move directory (un-nesting)
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Un-nesting directory...");
            var rootNested = fileSystem.FindDirectory(root, "mariadb-*");
            fileSystem.MoveDirectory(rootNested, root, cancellation);

            //configure
            _progress.ReportMessage("Configuring MariaDB...");
            var configFile = path.Combine(root, "my.ini");
            using (var config = new TransformingConfigFile(configFile, Constants.MariaDbConfig))
            {
                var rootDirBackslash = path.EnsureBackslashEnding(root);
                config.Transform("{MARIADB_INSTALL_ROOT}", rootDirBackslash);
            }
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
