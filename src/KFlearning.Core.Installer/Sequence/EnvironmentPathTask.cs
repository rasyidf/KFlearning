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

using System.IO;
using System.Threading;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Installer.Sequence
{
    public class EnvironmentPathTask : ITaskNode
    {
        private readonly IPathManager _path;
        private readonly IProgressBroker _progress;

        public string TaskName => "Setup Environment Variable";

        public EnvironmentPathTask(IPathManager path, IProgressBroker progress)
        {
            _path = path;
            _progress = progress;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var paths = new[]
            {
                Path.Combine(_path.GetModuleInstallPath(ModuleKind.Mingw), @"bin"),
            };

            _progress.ReportMessage("Processing environment variables...");
            for (int i = 0; i < paths.Length; i++)
            {
                if (definition.IsInstall)
                {
                    _path.AddVariable(paths[i]);
                }
                else
                {
                    _path.RemoveVariable(paths[i]);
                }

                _progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, paths.Length));
            }
        }
    }
}