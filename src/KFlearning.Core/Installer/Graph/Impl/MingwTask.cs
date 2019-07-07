using System;
using System.Collections.Generic;
using System.Threading;

namespace KFlearning.Core.Installer.Graph.Impl
{
    public class MingwTask : ITaskNode
    {
        private InstallMode _mode;

        public string TaskName => "MinGW configuration";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; }

        public MingwTask(InstallMode mode)
        {
            _mode = mode;
            Dependencies = new Queue<ITaskNode>();
            if (mode == InstallMode.Install)
            {
                Dependencies.Enqueue(new DownloadTask(null, ""));
                Dependencies.Enqueue(new ExtractTask("",""));
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(""));
            }
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                if (_mode == InstallMode.Install)
                {
                    InternalInstall();
                }
                else
                {
                    InternalUninstall();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void InternalInstall()
        {

        }

        private void InternalUninstall()
        {

        }
    }
}
