using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class MingwTask : ITaskNode
    {
        public string TaskName => "MinGW Compiler Suite";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var root = path.GetPath(PathKind.PathMingwRoot);

            // find zip and extract
            var mingwZip = path.FindFile(definition.DataPath, "mingw*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(mingwZip, root);
        }
    }
}
