using System;
using LiteDB;

namespace KFlearning.Core.Entities
{
    public class Article
    {
        [BsonId]
        public int ArticleId { get; set; }
        
        public string Series { get; set; }

        public DateTime Date { get; set; }
        
        public string Title { get; set; }
        
        public int Level { get; set; }
        
        public string Url { get; set; }

        public string SourceUrl { get; set; }
    }
}
