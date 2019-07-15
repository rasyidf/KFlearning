using KFlearning.Core.DAL;

namespace KFlearning.Core.Services
{
    public interface IProjectHandler
    {
        Project Create(string title, string path);
        void Import(Project project);
        void Destroy(Project project);
    }
}
