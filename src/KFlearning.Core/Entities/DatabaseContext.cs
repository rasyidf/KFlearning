using LiteDB;

namespace KFlearning.Core.Entities
{
    public class DatabaseContext : IDatabaseContext
    {
        public LiteDatabase Database { get; }

        public LiteCollection<Article> Articles => Database.GetCollection<Article>();

        public LiteCollection<Content> Contents => Database.GetCollection<Content>();

        public LiteCollection<Project> Projects => Database.GetCollection<Project>();

        public LiteCollection<Series> Series => Database.GetCollection<Series>();

        public DatabaseContext()
        {
            Database = new LiteDatabase("database.db");
        }
    }
}
