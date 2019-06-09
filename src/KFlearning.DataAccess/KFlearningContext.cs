using System.Data.Entity;
using KFlearning.DataAccess.Migrations;

namespace KFlearning.DataAccess
{
    public class KFlearningContext : DbContext
    {
        public DbSet<Post> SavedPosts { get; set; }
        public DbSet<Project> Projects { get; set; }

        public KFlearningContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KFlearningContext, Configuration>());
        }
    }
}
