// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProjectTypeItem.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.DAL;

namespace KFlearning.IDE.Models
{
    public class ProjectTypeItem
    {
        public ProjectTypeItem(ProjectType type, string displayName)
        {
            Type = type;
            DisplayName = displayName;
        }

        public string DisplayName { get; }

        public ProjectType Type { get; }
    }
}