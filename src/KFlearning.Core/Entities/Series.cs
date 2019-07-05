using LiteDB;

namespace KFlearning.Core.Entities
{
   public class Series
    {
        [BsonId]
        public string Title { get; set; }
    }
}
