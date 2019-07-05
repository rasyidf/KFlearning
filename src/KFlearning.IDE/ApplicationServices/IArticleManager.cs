// // PROJECT :
// // PROGRAM :
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// // TANGGAL :   05 Mei 2019

using System.Collections.Generic;
using System.Threading.Tasks;
using KFlearning.IDE.Models;

namespace KFlearning.IDE.ApplicationServices
{
    public interface IArticleManager
    {
        bool Online { get; set; }

        Task<IEnumerable<SeriesItem>> GetSeries();
        Task<IEnumerable<ArticleItem>> GetArticles(SeriesItem series);
        Task<IEnumerable<ArticleItem>> FindArticles(string title, SeriesItem series);
    }
}