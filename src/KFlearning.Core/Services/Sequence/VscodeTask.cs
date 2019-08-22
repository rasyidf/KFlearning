using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

namespace KFlearning.Core.Services.Sequence
{
    public class VscodeTask : ITaskNode
    {
        public string TaskName => "Visual Studio Code";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var process = definition.ResolveService<IProcessManager>();
            var root = path.GetPath(PathKind.PathVscodeRoot);

            // find zip and extract
            progress.ReportMessage("Extracting Visual Studio Code...");
            var vscodeZip = fileSystem.FindFile(definition.DataPath, "vscode-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(vscodeZip, root);
            }

            // create data directory
            fileSystem.CreateDirectory(path.Combine(root, "data"));
            fileSystem.CreateDirectory(path.Combine(root, @"data\user-data"));
            
            // install extensions
            progress.ReportMessage("Installing extensions...");
            var extensions = definition.VscodeExtensions;
            for (var i = 0; i < extensions.Count; i++)
            {
                process.RunWait(path.GetPath(PathKind.ExeVscode), $"--install-extension {extensions[i]}");
                progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, extensions.Count));
            }

            // save settings
            progress.ReportMessage("Configuring Visual Studio Code...");
            var vscodeSettingsFile = path.Combine(root, @"data\user-data\settings.json");
            using (var config = new TransformingConfigFile(vscodeSettingsFile, Constants.VscodeConfig))
            {
                var phpExePath = path.Combine(PathKind.PathPhpRoot, "php.exe");
                config.Transform("{PHP_PATH}", path.EnsureForwardSlash(phpExePath));
            }
        }
    }
}
