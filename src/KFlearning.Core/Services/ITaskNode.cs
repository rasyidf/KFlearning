using System.Threading;

namespace KFlearning.Core.Services
{
    public interface ITaskNode
    {
        string TaskName { get; }

        void Run(InstallDefinition definition, CancellationToken cancellation);
    }
}
