using KFlearning.DAL;
using System;
using KFlearning.API;

namespace KFlearning.IDE.Models
{
    public class ArticleItem
    {
        public DateTime Date { get; }
        
        public string Title { get; }

        public int Level { get; }

        public string Series { get; }

        public Uri Url { get; }

        public object Item { get; }

        public ArticleItem(Article article)
        {
            Item = article;
            Date = article.Date;
            Title = article.Title;
            Level = article.Level;
            Series = article.Series.Title;
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
    }
}
