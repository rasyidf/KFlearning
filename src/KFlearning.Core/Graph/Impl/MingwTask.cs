using System;
using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph.Impl
{
    public class MingwTask : ITaskNode
    {
        private readonly IPathManager _pathManager;
        private InstallMode _mode;

        public string TaskName => "MinGW configuration";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; private set; }
        
        public void Configure(InstallerDefinition definition)
        {
            _mode = definition.Mode;
            Dependencies = new Queue<ITaskNode>();
            if (_mode == InstallMode.Install)
            {
                
                Dependencies.Enqueue(new DownloadTask(definition.Packages.MingwUri, ""));
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
