using System;
using System.Collections.Generic;
using System.Threading;

namespace KFlearning.Core.Services.Sequence
{
    public class TaskGraph : ITaskGraph
    {
        #region Fields
        
        private readonly IProgressBroker _progressBroker;

        private CancellationTokenSource _tokenSource;
        private Thread _thread;
        private InstallerDefinition _definition;
        private Queue<ITaskNode> _sequence;

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

        public void RunSequence(InstallerDefinition definition, Queue<ITaskNode> sequence)
        {
            Cancel();
            _definition = definition;
            _sequence = sequence;

            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(ThreadCallback) {IsBackground = true};
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
                while (_sequence.Count > 0)
                {
                    var node = _sequence.Dequeue();
                    node.Run(_definition, _tokenSource.Token);

                    if (node is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _progressBroker.ReportMessage("OPERATION CANCELED.");
                _progressBroker.ReportSequenceProgress(100);
            }
            catch (Exception ex)
            {
                _progressBroker.ReportMessage("OPERATION FAULTED.\r\n" + ex);
                _progressBroker.ReportSequenceProgress(100);
            }
        }

        #endregion
    }
}
