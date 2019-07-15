using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class VscodeTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "Visual Studio Code";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var process = definition.ResolveService<IProcessManager>();
            var root = path.GetPath(PathKind.PathVscodeRoot);

            // find zip and extract
            _progress.ReportMessage("Extracting Visual Studio Code...");
            var vscodeZip = fileSystem.FindFile(definition.DataPath, "vscode-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(vscodeZip, root);
            extractor.StatusChanged -= Extractor_StatusChanged;

            // create data directory
            fileSystem.CreateDirectory(path.Combine(root, "data"));
            fileSystem.CreateDirectory(path.Combine(root, @"data\user-data"));
            
            // install extensions
            _progress.ReportMessage("Installing extensions...");
            var extensions = definition.Packages.VscodeExtensions;
            for (var i = 0; i < extensions.Count; i++)
            {
                process.RunWait(path.GetPath(PathKind.ExeVscode), $"--install-extension {extensions[i]}");
                _progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, extensions.Count));
            }

            // save settings
            _progress.ReportMessage("Configuring Visual Studio Code...");
            var vscodeSettingsFile = path.Combine(root, @"data\user-data\settings.json");
            using (var config = new TransformingConfigFile(vscodeSettingsFile, Constants.VscodeConfig))
            {
                var phpExePath = path.Combine(PathKind.PathPhpRoot, "php.exe");
                config.Transform("{PHP_PATH}", path.EnsureForwardSlash(phpExePath));
            }
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
