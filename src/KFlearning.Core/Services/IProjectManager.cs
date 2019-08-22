// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using KFlearning.Core.DAL;

#endregion

namespace KFlearning.Core.Services
{
    public interface IProjectManager
    {
        IEnumerable<Project> GetProjects();
        bool Exists(string title);
        void Create(string title);
        void Delete(Project project);
        void Export(Project project, string savePath);
        void Import(string zipFile);
        void Purge();

        bool CheckImportZip(string zipFile);
        string GetPathForProject(string title);
    }
}