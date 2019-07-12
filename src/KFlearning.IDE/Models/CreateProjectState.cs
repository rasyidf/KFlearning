// 
//  PROJECT  :   KFlearning
//  FILENAME :   CreateProjectState.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.DAL;

namespace KFlearning.IDE.Models
{
    public class CreateProjectState
    {
        public string Name { get; set; }

        public ProjectType Type { get; set; }
    }
}