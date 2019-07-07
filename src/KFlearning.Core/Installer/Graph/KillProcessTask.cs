using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace KFlearning.Core.Installer.Graph
{
    public class KillProcessTask : ITaskNode
    {
        private readonly string _processName;

        public string TaskName => "Kill process";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        public KillProcessTask(string processName)
        {
            _processName = processName;
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                var processes = Process.GetProcessesByName(_processName);
                if (processes.Any())
                {
                    foreach (Process process in processes)
                    {
                        process.Kill();
                        process.Dispose();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
