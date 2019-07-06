using System;

namespace KFlearning.Core.Installer
{
    public class MingwInstaller : IPackageInstaller
    {
        public string PackageName => "MinGW Compiler Suite";
        
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public void StartInstall()
        {
            throw new NotImplementedException();
        }

        public void StartUninstall()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
        
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}
