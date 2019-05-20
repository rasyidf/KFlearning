using KFlearning.ApplicationServices;

namespace KFlearning.DataAccess
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string RootPath { get; set; }

        public ProjectType Type { get; set; } 
    }
}
