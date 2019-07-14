using System;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class GlutTask : ITaskNode
    {
        public string TaskName => "GLUT Installation";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMingwRoot);

            // find zip and extract
            var glutZipPath = path.FindFile(definition.DataPath, "GLUT*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(glutZipPath, root);
            
            // install glut to system
            var glutDllSource = Path.Combine(Path.Combine(root, "glut32.dll"));
            var glutDllDest = Path.Combine(Environment.SystemDirectory, "glut32.dll");
            File.Move(glutDllSource, glutDllDest);
        }
    }
}
