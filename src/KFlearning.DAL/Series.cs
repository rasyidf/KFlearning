using System.ComponentModel.DataAnnotations;

namespace KFlearning.DAL
{
   public class Series
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
