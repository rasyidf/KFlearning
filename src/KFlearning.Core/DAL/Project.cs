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

using LiteDB;

#endregion

namespace KFlearning.Core.DAL
{
    public class Project
    {
        [BsonId] public int ProjectId { get; set; }

        // --- Common

        public string Title { get; set; }

        public string Path { get; set; }

        // --- Web Specific

        public bool VirtualHostEnabled { get; set; }

        public string VirtualHostDomain { get; set; }

        public string VirtualHostPath { get; set; }

        public string VirtualHostAlias { get; set; }

        public string VirtualHostAliasPath { get; set; }
    }
}