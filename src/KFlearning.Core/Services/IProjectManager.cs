﻿// 
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
        void Create(ProjectType type, string title);
        void Launch(Project project);
        void Delete(Project project);
        bool CheckImportZip(string zipFile);
        void Import(string zipFile);
        void Export(Project project, string savePath);

        void Purge();
        string GetPathForProject(string title);
    }
}