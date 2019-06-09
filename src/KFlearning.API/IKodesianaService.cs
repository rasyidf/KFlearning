using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KFlearning.API
{
    public interface IKodesianaService
    {
        Task<PostInfo> GetPostsAsync(CancellationToken cancellation, IEnumerable<int> category = null, IEnumerable<int> tags = null, int offset = 0);
        Task<IEnumerable<Taxonomy>> GetCategoriesAsync(CancellationToken cancellation, int offset = 0);
        Task<IEnumerable<Taxonomy>> GetTagsAsync(CancellationToken cancellation, int offset = 0);
        Task<Post> GetPostAsync(CancellationToken cancellation, int postId);       
    }
}