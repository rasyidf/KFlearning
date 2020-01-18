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


namespace KFlearning.Core.Services
{
    public class Template
    {
        public string Title { get; set; }

        public string ResourceName { get; set; }

        public Template(string title, string resourceName)
        {
            Title = title;
            ResourceName = resourceName;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}