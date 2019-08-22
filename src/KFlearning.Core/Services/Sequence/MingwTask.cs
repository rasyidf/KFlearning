using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

namespace KFlearning.Core.Services.Sequence
{
    public class MingwTask : ITaskNode
    {
        public string TaskName => "MinGW Compiler Suite";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMingwRoot);

            // find zip and extract
            progress.ReportMessage("Extracting MinGW...");
            var mingwZip = fileSystem.FindFile(definition.DataPath, "mingw-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(mingwZip, root);
            }
        }
    }
}
