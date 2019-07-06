// 
//  PROJECT  :   KFlearning
//  FILENAME :   DatabaseContext.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using LiteDB;

namespace KFlearning.Core.Entities
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