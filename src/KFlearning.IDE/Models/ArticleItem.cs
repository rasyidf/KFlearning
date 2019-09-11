// 
//  PROJECT  :   KFlearning
//  FILENAME :   ArticleItem.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using KFlearning.Core.API;
using KFlearning.Core.IDE.Data;

#endregion

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
            SourceUrl = article.SourceUrl;
        }

        public ArticleItem(Post post)
        {
            Item = post;
            Date = post.Date;
            Title = post.Title;
            Level = post.Level;
            Series = post.Series;
            Url = post.Url;
            SourceUrl = post.SourceUrl;
        }

        public DateTime Date { get; }

        public string Title { get; }

        public int Level { get; }

        public string Series { get; }

        public string Url { get; }

        public string SourceUrl { get; }

        public object Item { get; }
    }
}