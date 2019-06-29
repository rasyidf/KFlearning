using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KFlearning.DAL
{
    public class ArticleContent
    {
        [Key, ForeignKey("Article")]
        public int Id { get; set; }

        public string HtmlBody { get; set; }
    }
}
