using System.Data.Entity;

namespace KFlearning.DAL
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleContent> ArticleContents { get; set; }

        public DbSet<Series> Series { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));
        }
    }
}
