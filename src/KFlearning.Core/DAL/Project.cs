// 
//  PROJECT  :   KFlearning
//  FILENAME :   Project.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using LiteDB;

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