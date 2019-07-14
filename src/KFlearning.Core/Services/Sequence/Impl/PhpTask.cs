using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class PhpTask : ITaskNode
    {
        public string TaskName => "PHP Interpreter";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathPhpRoot);

            // find zip and extract
            var apacheZip = path.FindFile(definition.DataPath, "php-*.zip");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(apacheZip, root);

            // install xdebug
            var xdebugFile = path.FindFile(path.GetPathForTemp(), "php_xdebug*");
            var xdebugInstallPath = Path.Combine(root, "ext", Path.GetFileName(xdebugFile) ?? "");
            File.Move(xdebugFile, xdebugInstallPath);

            // save settings (php.ini)
            using (var config = new TransformingConfigFile(Path.Combine(root, "php.ini"), Constants.PhpConfig))
            {
                config.Transform("{XDEBUG_PATH}", path.EnsureForwardSlash(xdebugInstallPath));
            }
        }
    }
}
