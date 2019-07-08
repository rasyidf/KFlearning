using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class MariaDbTask : ITaskNode
    {
        #region Fields
        
        private IProgressBroker _broker;
        private IPathManager _pathManager;
        private InstallMode _mode;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "MariaDB";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; private set; }

        #endregion

        #region ITaskNode Methods
        
        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;

            Dependencies = new Queue<ITaskNode>();
            if (definition.Mode == InstallMode.Install)
            {
                var fileName = Path.GetFileName(definition.Packages.MariaDbUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);
                
                Dependencies.Enqueue(new DownloadTask(definition.Packages.MariaDbUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.MariaDbInstallRoot)));
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(_pathManager.GetPath(PathKind.MariaDbInstallRoot)));
            }
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportProgress(0);
                if (_mode == InstallMode.Install)
                {
                    _broker.ReportMessage("Installing MariaDB...");
                    InternalInstall();
                }
                else
                {
                    _broker.ReportMessage("Uninstalling MariaDB...");
                    InternalUninstall();
                }

                return true;
            }
            catch (Exception e)
            {
                _broker.ReportProgress(100);
                _broker.ReportMessage(e.ToString());
                return false;
            }
        } 

        #endregion

        #region Private Methods
        
        private void InternalInstall()
        {
            // find root directory
            var rootDir = Directory.GetDirectories(_pathManager.GetPath(PathKind.MariaDbInstallRoot), "*",
                SearchOption.TopDirectoryOnly).First();
            var rootDirBackslash = _pathManager.EnsureBackslashEnding(rootDir);

            // create config (my.ini)
            _broker.ReportProgress(50);
            _broker.ReportMessage("Configuring MariaDB...");

            var template = new StringBuilder(Constants.MariaDbConfig);
            template.Replace("{MARIADB_INSTALL_ROOT}", rootDirBackslash);

            var myiniPath = Path.Combine(rootDir, "my.ini");
            File.WriteAllText(myiniPath, template.ToString());

            // add to env vars
            _broker.ReportProgress(80);
            _broker.ReportMessage("Adding MariaDB to environment variable...");

            var path = Path.Combine(rootDir, "bin");
            _pathManager.AddPathEnvironmentVar(path);
        }

        private void InternalUninstall()
        {
            // remove from env vars
            _broker.ReportProgress(70);
            _broker.ReportMessage("Removing MariaDB from environment variable...");

            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.MariaDbInstallRoot));
        }

        #endregion
        
    }
}
