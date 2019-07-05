// // PROJECT :   KFlearning
// // FILENAME :  IArticleManager.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

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