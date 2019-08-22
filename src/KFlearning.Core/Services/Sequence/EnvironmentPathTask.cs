// 
//  PROJECT  :   KFlearning
//  FILENAME :   EnvironmentPathTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

#endregion

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
                path.GetPath(PathKind.PathVscodeRoot),
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