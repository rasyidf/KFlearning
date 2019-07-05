using LiteDB;

namespace KFlearning.Core.Entities
{
    public class Project
    {
        [BsonId]
        public int ProjectId { get; set; }

        // --- Common

        public string Title { get; set; }

        public ProjectType Type { get; set; }

        public string Path { get; set; }
        
        // --- Web Specific
        
        public string DomainName { get; set; }
    }
}
