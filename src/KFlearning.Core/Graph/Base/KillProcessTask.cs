using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class KillProcessTask : ITaskNode
    {
        #region Fields
        
        private readonly string _processName;
        private IProgressBroker _broker;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "Kill process";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        #endregion

        #region Constructor
        
        public KillProcessTask(string processName)
        {
            _processName = processName;
        }

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            _broker = definition.ResolveService<IProgressBroker>();
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                var processes = Process.GetProcessesByName(_processName);
                if (!processes.Any())
                {
                    _broker.ReportMessage("No process detected.");
                    return true;
                }

                for (var i = 0; i < processes.Length; i++)
                {
                    var progress = (int)Math.Round((double)(i + 1) / processes.Length * 100);
                    _broker.ReportProgress(progress);

                    Process process = processes[i];
                    process.Kill();
                    process.Dispose();
                }

                _broker.ReportMessage("All process killed.");
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
