// // PROJECT :
// // PROGRAM :
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// // TANGGAL :   05 Mei 2019

using System.Data.Entity;

namespace KFlearning.DAL
{
    public interface IDatabaseContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<ArticleContent> ArticleContents { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<Project> Projects { get; set; }
    }
}