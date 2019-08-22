using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

namespace KFlearning.Core.Services.Sequence
{
    public class GlutTask : ITaskNode
    {
        

        public string TaskName => "GLUT Installation";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            
            var root = path.GetPath(PathKind.PathMingwRoot);
            var extractPath = path.GetPathForTemp();

            // find zip and extract
            progress.ReportMessage("Extracting freeglut...");
            var glutZipPath = fileSystem.FindFile(definition.DataPath, "freeglut-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(glutZipPath, extractPath);
            }
            
            // install glut to MinGW
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Installing freeglut to MinGW...");
            fileSystem.CopyDirectory(path.Combine(extractPath, "freeglut"), root, cancellation);

            // install lib to MinGW 8.2.0
            var destPath = path.Combine(root, @"lib\gcc\mingw32\8.2.0");
            fileSystem.CopyDirectory(path.Combine(extractPath, @"freeglut\lib"), destPath, cancellation);
        }
    }
}
