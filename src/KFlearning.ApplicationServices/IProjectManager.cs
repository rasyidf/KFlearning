using System.Collections.Generic;
using KFlearning.ApplicationServices.Models;

namespace KFlearning.ApplicationServices
{
    public interface IProjectManager
    {
        IEnumerable<ProjectDefinition> GetProjects();
        void Create(ProjectType type, string title);
        void Delete(ProjectDefinition project);
        void Export(ProjectDefinition project, string savePath);
        void Import(string zipFile);

        string GetPathForProject(string title);
    }
}