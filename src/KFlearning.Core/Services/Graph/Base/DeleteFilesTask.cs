using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class DeleteFilesTask : ITaskNode
    {
        #region Fields
        
        private IPathManager _pathManager;
        private IProgressBroker _broker;
        private readonly string _path;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "Delete files";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        #endregion

        #region Constructor
        
        public DeleteFilesTask(string path)
        {
            _path = path;
        }

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportMessage("Deleting files...");
                if (!Directory.Exists(_path) && !File.Exists(_path))
                {
                    _broker.ReportMessage("No file or folder to delete.");
                    _broker.ReportProgress(100);
                    return false;
                }

                _broker.ReportProgress(-1);
                if (Directory.Exists(_path))
                {
                    _pathManager.RecursiveDelete(_path, cancellation);
                }
                else
                {
                    File.Delete(_path);
                }

                _broker.ReportMessage("All files or folders deleted.");
                _broker.ReportProgress(100);
                return true;
            }
            catch (Exception e)
            {
                _broker.ReportMessage(e.ToString());
                _broker.ReportProgress(100);
                return false;
            }
        } 

        #endregion
    }
}
