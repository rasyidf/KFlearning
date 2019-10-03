using System;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services
{
    public class KflearningShortcut : IInstallable
    {
        private readonly IFileSystemManager _fileSystem;
        private readonly IPathManager _path;

        public KflearningShortcut(IFileSystemManager fileSystem, IPathManager path)
        {
            _fileSystem = fileSystem;
            _path = path;
        }

        public void Install(Action<int> progressCallback, CancellationToken cancellation)
        {
            var exePath = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"KFlearning.exe");
            _fileSystem.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);
        }

        public void Uninstall(Action<int> progressCallback, CancellationToken cancellation)
        {
            var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "KFlearning.lnk");
            File.Delete(shortcutPath);
        }
    }
}
