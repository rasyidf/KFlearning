using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.Installer.Graph.Impl;

namespace KFlearning.Core.Installer.Graph
{
    class InstallGraph : ITaskNode
    {
        public string TaskName => "Install graph root";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; }

        public InstallGraph()
        {
            Dependencies = new Queue<ITaskNode>();
            Dependencies.Enqueue(new MingwTask(InstallMode.Install));
            //....
            Dependencies.Enqueue(new MariaDbTask());
        }

        public bool Run(CancellationToken cancellation)
        {
            return true;
        }
    }
}
