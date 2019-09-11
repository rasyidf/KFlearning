// 
//  PROJECT  :   KFlearning
//  FILENAME :   KflearningTask.cs
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
    public class KflearningTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IModuleService _moduleService;
        private readonly IPathManager _path;

        public string TaskName => "KFlearning IDE";

        public KflearningTask(IProgressBroker progress, IModuleService moduleService, IPathManager path)
        {
            _progress = progress;
            _moduleService = moduleService;
            _path = path;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            // find zip and extract
            _progress.ReportMessage("Extracting KFlearning...");
            var kflearningZip = _moduleService.GetModuleZipPath(ModuleZipFile.Ide);
            using (var extractor = new ZipExtractor((s, e) => _progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                var kflearningRoot = _path.GetModuleInstallPath(ModuleKind.Ide);
                extractor.ExtractAll(kflearningZip, kflearningRoot);
            }
        }
    }
}