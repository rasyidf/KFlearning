using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public interface ITaskNode
    {
        string TaskName { get; }
        bool HasDependencies { get; }
        Queue<ITaskNode> Dependencies { get; }

        void Configure(InstallerDefinition definition);
        bool Run(CancellationToken cancellation);
    }
}
