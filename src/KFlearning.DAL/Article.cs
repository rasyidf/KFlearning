using System;
using System.ComponentModel.DataAnnotations;

namespace KFlearning.DAL
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public DateTime Date { get; set; }
        
        public string Title { get; set; }

        public Series Series { get; set; }

        public int Level { get; set; }

        public virtual ArticleContent Content { get; set; }

        public Uri Url { get; set; }
    }
}
