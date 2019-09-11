// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectItem.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using KFlearning.Core.IDE.Data;

#endregion

namespace KFlearning.IDE.Models
{
    public class ProjectItem
    {
        public ProjectItem(Project project)
        {
            Item = project;
        }

        public string Title => Item.Title;

        public string Path => Item.Path;

        public Project Item { get; }
    }
}