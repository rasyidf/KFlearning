// // PROJECT :
// // PROGRAM :
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// // TANGGAL :   05 Mei 2019

using System.Collections.Generic;
using KFlearning.Core.Entities;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IProjectManager
    {
        IEnumerable<ProjectItem> GetProjects();
        void Create(ProjectType type, string title);
        void Launch(Project project);
        void Delete(Project project);
        void Import(string zipFile);
        void Export(Project project, string savePath);

        void Purge();
        string GetPathForProject(string title);
    }
}