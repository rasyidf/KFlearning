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
using KFlearning.Core.Services.Installer;

#endregion

namespace KFlearning.Core.Services.Sequence
{
    public class KflearningTask : ITaskNode
    {
        public string TaskName => "KFlearning IDE";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();

            // find zip and extract
            progress.ReportMessage("Extracting KFlearning...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "kflearning-ide-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(apacheZip, path.GetPath(PathKind.PathKflearningRoot));
            }
        }
    }
}