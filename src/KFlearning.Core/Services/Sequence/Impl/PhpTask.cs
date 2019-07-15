using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class PhpTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "PHP Interpreter";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathPhpRoot);

            // find zip and extract
            _progress.ReportMessage("Extracting PHP...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "php-*.zip");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(apacheZip, root);
            extractor.StatusChanged -= Extractor_StatusChanged;

            // install xdebug
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Installing Xdebug to PHP...");
            var xdebugFile = fileSystem.FindFile(definition.DataPath, "php_xdebug*");
            var xdebugInstallPath = path.Combine(root, "ext", path.GetFileName(xdebugFile) ?? "");
            fileSystem.CopyFile(xdebugFile, xdebugInstallPath);

            // save settings (php.ini)
            _progress.ReportMessage("Configuring PHP...");
            using (var config = new TransformingConfigFile(path.Combine(root, "php.ini"), Constants.PhpConfig))
            {
                config.Transform("{XDEBUG_PATH}", path.EnsureForwardSlash(xdebugInstallPath));
            }
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
