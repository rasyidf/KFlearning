using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class GlutTask : ITaskNode
    {
        private IProgressBroker _progress;

        public string TaskName => "GLUT Installation";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            _progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            
            var root = path.GetPath(PathKind.PathMingwRoot);
            var extractPath = path.GetPathForTemp();

            // find zip and extract
            _progress.ReportMessage("Extracting freeglut...");
            var glutZipPath = fileSystem.FindFile(definition.DataPath, "freeglut-*");
            var extractor = new ZipExtractor();
            extractor.StatusChanged += Extractor_StatusChanged;
            extractor.ExtractAll(glutZipPath, extractPath);
            extractor.StatusChanged -= Extractor_StatusChanged;
            
            // install glut to MinGW
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Installing freeglut to MinGW...");
            fileSystem.CopyDirectory(path.Combine(extractPath, "freeglut"), root, cancellation);

            // install lib to MinGW 8.2.0
            var destPath = path.Combine(root, @"lib\gcc\mingw32\8.2.0");
            fileSystem.CopyDirectory(path.Combine(extractPath, @"freeglut\lib"), destPath, cancellation);
        }

        private void Extractor_StatusChanged(object sender, ZipExtractEventArgs e)
        {
            _progress.ReportNodeProgress(e.ProgressPercentage);
        }
    }
}
