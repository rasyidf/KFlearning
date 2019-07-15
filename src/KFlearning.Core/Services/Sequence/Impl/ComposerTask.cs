using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class ComposerTask : ITaskNode
    {
        public string TaskName => "Composer Installation";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var path = definition.ResolveService<IPathManager>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();

            // install composer
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Installing composer...");
            var composerFile = path.Combine(definition.DataPath, "composer.phar");
            var composerInstallPath = path.Combine(PathKind.PathComposerRoot, "composer.phar");
            fileSystem.CopyFile(composerFile, composerInstallPath);

            // create batch execution file
            var composerBatchPath = path.Combine(PathKind.PathComposerRoot, "composer.bat");
            fileSystem.WriteFile(composerBatchPath, Constants.ComposerBatch);
        }
    }
}
