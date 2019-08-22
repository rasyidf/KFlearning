// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProjectManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using KFlearning.Core.DAL;

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