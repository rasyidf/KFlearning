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
            Database = new LiteDatabase("database.db");
        }

        public LiteDatabase Database { get; }

        public LiteCollection<Article> Articles => Database.GetCollection<Article>();

        public LiteCollection<Content> Contents => Database.GetCollection<Content>();

        public LiteCollection<Project> Projects => Database.GetCollection<Project>();

        public LiteCollection<Series> Series => Database.GetCollection<Series>();
    }
}