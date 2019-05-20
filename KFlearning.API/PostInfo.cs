using System.Collections.Generic;

namespace KFlearning.API
{
    public class PostInfo
    {
        public int TotalPage { get; }

        public int Offset { get; set; }

        public List<Post> Posts { get; }

        public PostInfo(int totalPage, int offset, List<Post> posts)
        {
            TotalPage = totalPage;
            Offset = offset;
            Posts = posts;
        }
    }
}
