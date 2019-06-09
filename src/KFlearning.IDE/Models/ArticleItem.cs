using System;

namespace KFlearning.IDE.Models
{
    public class ArticleItem
    {
        public int Id { get; }

        public string Title { get; }

        public DateTime ReleaseDate { get; }

        public string Category { get; }

        public int Level { get; }

        public ArticleItem(int id, string title, DateTime releaseDate, string category, int level)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Category = category;
            Level = level;
            Id = id;
        }
    }
}
