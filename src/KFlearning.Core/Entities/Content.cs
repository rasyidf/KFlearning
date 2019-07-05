using LiteDB;

namespace KFlearning.Core.Entities
{
    public class Content
    {
        [BsonId]
        public int ContentId { get; set; }

        public int ArticleId { get; set; }

        public string HtmlBody { get; set; }
    }
}
