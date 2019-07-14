using System.Threading;

namespace KFlearning.Core.Services.Graph
{
    public interface ITaskNode
    {
        string TaskName { get; }

        void Run(InstallerDefinition definition, CancellationToken cancellation);
    }
}
