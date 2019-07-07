using System.Collections.Generic;
using System.Threading;

namespace KFlearning.Core.Installer.Graph
{
    public interface ITaskNode
    {
        string TaskName { get; }
        bool HasDependencies { get; }
        Queue<ITaskNode> Dependencies { get; }

        bool Run(CancellationToken cancellation);
    }
}
