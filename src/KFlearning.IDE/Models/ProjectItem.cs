// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectItem.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.DAL;

namespace KFlearning.IDE.Models
{
    public class ProjectItem
    {
        public ProjectItem(Project project)
        {
            Item = project;
        }

        public ProjectType Type => Item.Type;

        public string Title => Item.Title;

        public string Path => Item.Path;

        public Project Item { get; }
    }
}