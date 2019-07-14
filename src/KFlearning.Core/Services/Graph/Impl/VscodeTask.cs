using System;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class VscodeTask : ITaskNode
    {
        public string TaskName => "Visual Studio Code";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var process = definition.ResolveService<IProcessManager>();
            var root = path.GetPath(PathKind.PathVscodeRoot);

            // find zip and extract
            var vscodeZip = path.FindFile(definition.DataPath, "vscode*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(vscodeZip, root);
            
            // create data directory
            Directory.CreateDirectory(Path.Combine(root, "data"));
            Directory.CreateDirectory(Path.Combine(root, @"data\user-data"));
            
            // install extensions
            var extensions = definition.Packages.VscodeExtensions;
            for (var i = 0; i < extensions.Count; i++)
            {
                var percentage = (int) Math.Round((double) (i + 1) / extensions.Count * 100, 0);
                process.RunWait(path.GetPath(PathKind.ExeVscode), $"--install-extension {extensions[i]}");
            }

            // save settings
            var vscodeSettingsFile = Path.Combine(root, @"data\user-data\settings.json");
            using (var config = new TransformingConfigFile(vscodeSettingsFile, Constants.VscodeConfig))
            {
                var phpExePath = Path.Combine(path.GetPath(PathKind.PathPhpRoot), "php.exe");
                config.Transform("{PHP_PATH}", path.EnsureForwardSlash(phpExePath));
            }
        }
    }
}
