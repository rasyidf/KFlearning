// // PROJECT :   KFlearning
// // FILENAME :  ArticleItem.cs
// // AUTHOR  :   Fahmi Noor Fiqri
// // NPM     :   065118116
// //
// // This file is part of KFlearning, licensed under MIT license.

using System;
using KFlearning.API;
using KFlearning.Core.Entities;

namespace KFlearning.IDE.Models
{
    public class ArticleItem
    {
        public ArticleItem(Article article)
        {
            Item = article;
            Date = article.Date;
            Title = article.Title;
            Level = article.Level;
            Series = article.Series;
            Url = article.Url;
        }

        public ArticleItem(Post post)
        {
            Item = post;
            Date = post.Date;
            Title = post.Title;
            Level = post.Level;
            Series = post.Series;
            Url = post.Url;
        }

        public DateTime Date { get; }

        public string Title { get; }

        public int Level { get; }

        public string Series { get; }

        public string Url { get; }

        public object Item { get; }
    }
}