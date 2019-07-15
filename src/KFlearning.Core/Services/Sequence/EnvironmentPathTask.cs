using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class EnvironmentPathTask : ITaskNode
    {
        private readonly bool _install;

        public string TaskName => "Setup Environment Variable";

        public EnvironmentPathTask(bool install)
        {
            _install = install;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var path = definition.ResolveService<IPathManager>();

            var paths = new[]
            {
                path.Combine(PathKind.PathMingwRoot, "bin"),
                path.GetPath(PathKind.PathMariaDbRoot),
                path.GetPath(PathKind.PathApacheRoot),
                path.GetPath(PathKind.PathVscodeRoot),
                path.GetPath(PathKind.PathPhpRoot),
                path.GetPath(PathKind.PathComposerRoot)
            };

            progress.ReportMessage("Processing environment variables...");
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

                progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, paths.Length));
            }
        }
    }
}
