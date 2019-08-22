using System.Threading;

namespace KFlearning.Core.Services.Installer
{
    public interface ITaskNode
    {
        string TaskName { get; }

        void Run(InstallDefinition definition, CancellationToken cancellation);
    }
}
