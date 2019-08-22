// 
//  PROJECT  :   KFlearning
//  FILENAME :   TaskGraph.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services.Installer
{
    public class TaskGraph : ITaskGraph
    {
        #region Fields

        private readonly IProgressBroker _progressBroker;
        private readonly IPathManager _pathManager;

        private CancellationTokenSource _tokenSource;
        private Thread _thread;
        private InstallDefinition _definition;
        private Queue<ITaskNode> _sequence;

        #endregion

        #region Properties

        public bool IsRunning => _tokenSource != null;

        #endregion

        #region Constructor

        public TaskGraph(IProgressBroker progressBroker, IPathManager pathManager)
        {
            _progressBroker = progressBroker;
            _pathManager = pathManager;
        }

        #endregion

        #region Public Methods

        public bool IsInstalled()
        {
            return Directory.Exists(_pathManager.Combine(PathKind.PathBase, "ide"));
        }

        public void RunSequence(InstallDefinition definition, Queue<ITaskNode> sequence)
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

                    _progressBroker.ReportMessage($"RUNNING - {node.TaskName}");
                    node.Run(_definition, _tokenSource.Token);

                    _progressBroker.ReportMessage($"FINISHED - {node.TaskName}");
                    _progressBroker.ReportSequenceProgress(MathHelper.CalculatePercentage(++count, total));
                }

                _progressBroker.ReportMessage("FINISHED");
                _progressBroker.ReportSequenceProgress(100);
            }
            catch (OperationCanceledException)
            {
                _progressBroker.ReportMessage("CANCELED");
                _progressBroker.ReportSequenceProgress(100);
            }
            catch (Exception ex)
            {
                _progressBroker.ReportMessage($"FAULTED{Environment.NewLine}{ex}");
                _progressBroker.ReportSequenceProgress(100);
            }
        }

        #endregion
    }
}