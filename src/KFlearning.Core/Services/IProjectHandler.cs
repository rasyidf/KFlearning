﻿// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProjectHandler.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using KFlearning.Core.DAL;

#endregion

namespace KFlearning.Core.Services
{
    public interface IProjectHandler
    {
        void Launch(Project project);
        void CreateAlias(Project project);
        void RemoveAlias(Project project);
        void SaveMetadata(Project project);
        void InitializeCpp(Project project);
    }
}