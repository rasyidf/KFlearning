using System;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;
using KFlearning.Core.Services.Installer;

namespace KFlearning.Core.Services.Sequence
{
    public class KflearningShortcutTask : ITaskNode
    {
        private bool _isInstall;

        public string TaskName => "KFlearning Shortcut";


        public KflearningShortcutTask(bool isInstall)
        {
            _isInstall = isInstall;
        }
        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var path = definition.ResolveService<IPathManager>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();

            if (_isInstall)
            {
                progress.ReportNodeProgress(-1);
                progress.ReportMessage("Creating desktop shortcut...");
                var exePath = path.Combine(PathKind.PathKflearningRoot, "KFlearning.IDE.exe");
                fileSystem.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);
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
