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

using LiteDB;

namespace KFlearning.Core.IDE.Data
{
    public class Project
    {
        [BsonId] public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }
    }
}