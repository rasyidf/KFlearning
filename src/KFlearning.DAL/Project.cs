using System.ComponentModel.DataAnnotations;

namespace KFlearning.DAL
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public string Path { get; set; }

        public ProjectType Type { get; set; }

        public string Domain { get; set; }
    }
}
