// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProjectHandler.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using KFlearning.Core.IDE.Data;

#endregion

namespace KFlearning.Core.IDE
{
    public interface IProjectHandler
    {
        void Launch(Project project);
        void CreateLink(Project project);
        void RemoveLink(Project project);
        bool LinkExists(Project project);
        bool CanModifyLink();
        void SaveMetadata(Project project);
        void InitializeCpp(Project project);
    }
}