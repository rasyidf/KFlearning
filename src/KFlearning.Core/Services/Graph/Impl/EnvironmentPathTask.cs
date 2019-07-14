using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class EnvironmentPathTask : ITaskNode
    {
        private readonly bool _install;

        public string TaskName => "Add environment variables...";

        public EnvironmentPathTask(bool install)
        {
            _install = install;
        }

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();

            var paths = new[]
            {
                Path.Combine(path.GetPath(PathKind.PathMingwRoot), "bin"),
                path.GetPath(PathKind.PathMariaDbRoot),
                path.GetPath(PathKind.PathApacheRoot),
                path.GetPath(PathKind.PathVscodeRoot),
                path.GetPath(PathKind.PathPhpRoot)
            };

            for (int i = 0; i < paths.Length; i++)
            {
                if (_install)
                {
                    path.AddPathEnvironmentVar(paths[i]);
                }
                else
                {
                    path.RemovePathEnvironmentVar(paths[i]);
                }
            }
        }
    }
}
