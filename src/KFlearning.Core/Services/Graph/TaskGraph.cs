using System;
using System.Threading;

namespace KFlearning.Core.Services.Graph
{
    public class TaskGraph : ITaskGraph
    {
        #region Fields
        
        private readonly IProgressBroker _progressBroker;

        private CancellationTokenSource _tokenSource;
        private Thread _thread;
        private InstallerDefinition _definition;
        private ITaskNode _rootNode;

        #endregion

        #region Properties
        
        public bool IsRunning => _tokenSource != null;

        #endregion

        #region Constructor
        
        public TaskGraph(IProgressBroker progressBroker)
        {
            _progressBroker = progressBroker;
        }

        #endregion

        #region Public Methods

        public void RunGraph(InstallerDefinition definition, ITaskNode rootNode)
        {
            Cancel();
            _definition = definition;
            _rootNode = rootNode;

            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(ThreadCallback);
            _thread.Start();
        }

        public void Cancel()
        {
            if (_tokenSource == null) return;
            _tokenSource.Dispose();
            _tokenSource = null;

            _thread.Join();
            _thread = null;
        }

        #endregion

        #region Private Methods

        private void ThreadCallback()
        {
            try
            {
                InternalRunGraph(_definition, _rootNode);
            }
            catch (OperationCanceledException)
            {
                _progressBroker.ReportMessage("OPERATION CANCELED.");
                _progressBroker.ReportProgress(100);
            }
            catch (Exception ex)
            {
                _progressBroker.ReportMessage("OPERATION FAULTED.\r\n" + ex);
                _progressBroker.ReportProgress(100);
            }
        }

        private void InternalRunGraph(InstallerDefinition definition, ITaskNode rootNode)
        {
            _progressBroker.ReportMessage("Running node: " + rootNode.TaskName);
            rootNode.Configure(definition);
            if (rootNode.HasDependencies)
            {
                _progressBroker.ReportMessage($"Node has {rootNode.Dependencies.Count} dependencies, running dependencies...");
                foreach (ITaskNode nodeDependency in rootNode.Dependencies)
                {
                    _progressBroker.ReportMessage("Running dependency: " + rootNode.TaskName);
                    InternalRunGraph(definition, nodeDependency);
                }
            }

            rootNode.Run(_tokenSource.Token);
            _progressBroker.ReportMessage("Node finished: " + rootNode.TaskName);

            if (!(rootNode is IDisposable disposable)) return;
            disposable.Dispose();
            _progressBroker.ReportMessage("Node disposed: " + rootNode.TaskName);
        } 

        #endregion
    }
}
