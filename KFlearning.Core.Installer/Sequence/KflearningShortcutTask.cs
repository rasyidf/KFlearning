using System;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Installer.Sequence
{
    public class KflearningShortcutTask : ITaskNode
    {
        private readonly IProgressBroker _progress;
        private readonly IFileSystemManager _fileSystem;
        private readonly IPathManager _path;

        public string TaskName => "KFlearning Shortcut";

        public KflearningShortcutTask(IProgressBroker progress, IFileSystemManager fileSystem, IPathManager path)
        {
            _progress = progress;
            _fileSystem = fileSystem;
            _path = path;
        }

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            if (definition.IsInstall)
            {
                _progress.ReportNodeProgress(-1);
                _progress.ReportMessage("Creating desktop shortcut...");
                var exePath = Path.Combine(_path.GetModuleInstallPath(ModuleKind.Ide), @"KFlearning.IDE.exe");
                _fileSystem.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);
            }
            else
            {
                var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    "KFlearning.lnk");
                File.Delete(shortcutPath);
            }
        }
    }
}
