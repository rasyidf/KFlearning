using KFlearning.Core.DAL;

namespace KFlearning.Core.Services
{
    public interface IProjectHandler
    {
        Project Initialize(string title, string path);
        void Initialize(Project project);
        void Uninitialize(Project project);
    }
}
