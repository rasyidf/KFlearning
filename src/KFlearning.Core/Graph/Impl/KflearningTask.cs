using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class KflearningTask : ITaskNode
    {
        #region Fields
        
        private IProgressBroker _broker;
        private IPathManager _pathManager;
        private InstallMode _mode;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "KFlearning IDE";
        public bool HasDependencies => _mode == InstallMode.Install;
        public Queue<ITaskNode> Dependencies { get; private set; }

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;

            if (definition.Mode != InstallMode.Install) return;
            var fileName = Path.GetFileName(definition.Packages.KflearningUri.AbsoluteUri);
            var savePath = _pathManager.GetPathForTemp(fileName);

            Dependencies = new Queue<ITaskNode>();
            Dependencies.Enqueue(new DownloadTask(definition.Packages.KflearningUri, savePath));
            Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.PathKflearningRoot)));
        }

        public bool Run(CancellationToken cancellation)
        {
            if (_mode == InstallMode.Uninstall) return true;
            try
            {
                _pathManager.InitializePaths();
                _broker.ReportProgress(-1);

                var exePath = Path.Combine(_pathManager.GetPath(PathKind.PathKflearningRoot), "KFlearning.IDE.exe");
                _pathManager.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);

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
    }
}
