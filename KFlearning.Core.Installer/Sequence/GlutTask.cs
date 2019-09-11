// 
//  PROJECT  :   KFlearning
//  FILENAME :   GlutTask.cs
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
    public class GlutTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IFileSystemManager _fileSystem;
        private readonly IPathManager _path;
        private readonly IModuleService _module;

        public string TaskName => "GLUT Installation";

        public GlutTask(IProgressBroker progress, IFileSystemManager fileSystem, IPathManager path, IModuleService module)
        {
            _progress = progress;
            _fileSystem = fileSystem;
            _path = path;
            _module = module;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var root = _path.GetModuleInstallPath(ModuleKind.Mingw);
            var extractPath = _path.GetPathForTemp();

            // find zip and extract
            _progress.ReportMessage("Extracting freeglut...");
            var glutZipPath = _module.GetModuleZipPath(ModuleZipFile.Glut);
            using (var extractor = new ZipExtractor((s, e) => _progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(glutZipPath, extractPath);
            }

            // install glut to MinGW
            _progress.ReportNodeProgress(-1);
            _progress.ReportMessage("Installing freeglut to MinGW...");

            var glutRoot = Path.Combine(extractPath, "freeglut");
            _fileSystem.CopyDirectory(glutRoot, root, cancellation);

            // install lib to MinGW 8.2.0
            var sourcePath = Path.Combine(extractPath, @"freeglut\lib");
            var destPath = Path.Combine(root, @"lib\gcc\mingw32\8.2.0");
            _fileSystem.CopyDirectory(sourcePath, destPath, cancellation);
        }
    }
}