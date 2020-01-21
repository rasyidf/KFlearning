// 
//  PROJECT  :   KFlearning
//  FILENAME :   Project.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

#endregion


using System.Collections.Generic;

namespace KFlearning.Core.Services
{
    public class Template
    {
        public string Title { get; }

        public List<Transformable> FileMapping { get; }

        public Template(string title, List<Transformable> fileMapping)
        {
            Title = title;
            FileMapping = fileMapping;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}