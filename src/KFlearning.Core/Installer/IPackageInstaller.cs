using System;

namespace KFlearning.Core.Installer
{
    public interface IPackageInstaller
    {
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        string PackageName { get; }

        void StartInstall();
        void StartUninstall();
        void Stop();
    }
}
