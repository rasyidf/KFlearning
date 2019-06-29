using System.Collections.Generic;
using KFlearning.DAL;

namespace KFlearning.Core.IO
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetProjects();
        void Create(ProjectType type, string title);
        void Delete(Project project);
        void Export(Project project, string savePath);
        void Import(string zipFile);

        string GetPathForProject(string title);
    }
}