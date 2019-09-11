// 
//  PROJECT  :   KFlearning
//  FILENAME :   DatabaseContext.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.IO;
using KFlearning.Core.Resources;
using LiteDB;

#endregion

namespace KFlearning.Core.IDE.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        public DatabaseContext(IPathService path)
        {
            Database = new LiteDatabase(path.GetDatabasePath());
        }

        public LiteDatabase Database { get; }

        public LiteStorage Storage => Database.FileStorage;

        public LiteCollection<Article> Articles => Database.GetCollection<Article>();

        public LiteCollection<Project> Projects => Database.GetCollection<Project>();

        public LiteCollection<Series> Series => Database.GetCollection<Series>();
    }
}