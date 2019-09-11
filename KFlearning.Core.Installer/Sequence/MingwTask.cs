// 
//  PROJECT  :   KFlearning
//  FILENAME :   MingwTask.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Threading;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Installer.Sequence
{
    public class MingwTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IModuleService _moduleService;
        private readonly IPathManager _path;

        public string TaskName => "MinGW Compiler Suite";

        public MingwTask(IProgressBroker progress, IModuleService moduleService, IPathManager path)
        {
            _progress = progress;
            _moduleService = moduleService;
            _path = path;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var root = _path.GetModuleInstallPath(ModuleKind.Mingw);

            // find zip and extract
            _progress.ReportMessage("Extracting MinGW...");
            var mingwZip = _moduleService.GetModuleZipPath(ModuleZipFile.Mingw);
            using (var extractor = new ZipExtractor((s, e) => _progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(mingwZip, root);
            }
        }
    }
}