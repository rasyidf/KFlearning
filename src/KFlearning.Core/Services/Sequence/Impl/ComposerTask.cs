using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class ComposerTask : ITaskNode
    {
        public string TaskName => "Composer Installation";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();

            // install composer
            var composerFile = Path.Combine(definition.DataPath, "composer.phar");
            var composerInstallPath = Path.Combine(path.GetPath(PathKind.PathComposerRoot), @"composer.phar");
            File.Move(composerFile, composerInstallPath);

            // create batch execution file
            var composerBatchPath = Path.Combine(path.GetPath(PathKind.PathComposerRoot), @"composer.bat");
            File.WriteAllText(composerBatchPath, Constants.ComposerBatch);
        }
    }
}
