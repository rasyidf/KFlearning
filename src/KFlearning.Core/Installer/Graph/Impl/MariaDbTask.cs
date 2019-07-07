using System;
using System.Collections.Generic;
using System.Threading;

namespace KFlearning.Core.Installer.Graph.Impl
{
    public class MariaDbTask : ITaskNode
    {
        public string TaskName => "MariaDB configuration";
        public bool HasDependencies { get; }
        public Queue<ITaskNode> Dependencies { get; }

        public bool Run(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
