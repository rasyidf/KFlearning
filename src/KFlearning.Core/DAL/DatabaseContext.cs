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

using LiteDB;

#endregion

namespace KFlearning.Core.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        public DatabaseContext()
        {
            Database = new LiteDatabase(Constants.DatabaseConnectionString);
        }

        public LiteDatabase Database { get; }

        public LiteStorage Storage => Database.FileStorage;

        public LiteCollection<Article> Articles => Database.GetCollection<Article>();

        public LiteCollection<Project> Projects => Database.GetCollection<Project>();

        public LiteCollection<Series> Series => Database.GetCollection<Series>();
    }
}