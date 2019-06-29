using System.Data.Entity;
using SQLite.CodeFirst;

namespace KFlearning.DAL
{
    public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<DatabaseContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
    }
}
