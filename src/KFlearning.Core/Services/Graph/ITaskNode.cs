using System.Collections.Generic;
using System.Threading;

namespace KFlearning.Core.Services.Graph
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
