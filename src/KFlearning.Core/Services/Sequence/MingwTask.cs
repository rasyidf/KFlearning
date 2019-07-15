using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class MingwTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "MinGW Compiler Suite";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMingwRoot);

            // find zip and extract
            _progress.ReportMessage("Extracting MinGW...");
            var mingwZip = fileSystem.FindFile(definition.DataPath, "mingw-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(mingwZip, root);
            extractor.StatusChanged += Extractor_StatusChanged;
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
