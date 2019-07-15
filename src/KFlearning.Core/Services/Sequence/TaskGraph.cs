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
                int count = 0, total = _sequence.Count;
                while (_sequence.Count > 0)
                {
                    _tokenSource.Token.ThrowIfCancellationRequested();
                    var node = _sequence.Dequeue();

                    _progressBroker.ReportMessage("[ RUNNING ] " + node.TaskName);
                    node.Run(_definition, _tokenSource.Token);

                    if (node is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    _progressBroker.ReportMessage("[ FINISHED ] " + node.TaskName);
                    _progressBroker.ReportSequenceProgress(MathHelper.CalculatePercentage(++count, total));
                }

                _progressBroker.ReportMessage("[ FINISHED ]");
                _progressBroker.ReportSequenceProgress(100);
            }
            catch (OperationCanceledException)
            {
                _progressBroker.ReportMessage("[ CANCELED ]");
                _progressBroker.ReportSequenceProgress(100);
            }
            catch (Exception ex)
            {
                _progressBroker.ReportMessage($"[ FAULTED ]{Environment.NewLine}{ex}");
                _progressBroker.ReportSequenceProgress(100);
            }
        }

        #endregion
    }
}
