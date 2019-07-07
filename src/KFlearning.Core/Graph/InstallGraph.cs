using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.Graph.Impl;

namespace KFlearning.Core.Graph
{
    public class InstallGraph : ITaskNode
    {
        public string TaskName => "Install graph root";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; }

        public InstallGraph(MingwTask mingw, MariaDbTask mariaDb)
        {
            Dependencies = new Queue<ITaskNode>();
            Dependencies.Enqueue(mingw);
            //....
            Dependencies.Enqueue(mariaDb);
        }

        public void Configure(dynamic parameters)
        {
        }

        public bool Run(CancellationToken cancellation)
        {
            return true;
        }
    }
}
