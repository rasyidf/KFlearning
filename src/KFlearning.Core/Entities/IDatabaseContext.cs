// // PROJECT :
// // PROGRAM :
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// // TANGGAL :   05 Mei 2019

using LiteDB;

namespace KFlearning.Core.Entities
{
    public interface IDatabaseContext
    {
        LiteDatabase Database { get; }
        LiteCollection<Article> Articles { get; }
        LiteCollection<Content> Contents { get; }
        LiteCollection<Project> Projects { get; }
        LiteCollection<Series> Series { get; }
    }
}