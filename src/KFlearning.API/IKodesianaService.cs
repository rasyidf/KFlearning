using System.Collections.Generic;
using System.Threading.Tasks;

namespace KFlearning.API
{
    public interface IKodesianaService
    {
        Task<bool> IsOnline();
        Task<IEnumerable<Post>> GetPostsAsync(string series = null);     
        Task<IEnumerable<Post>> FindPostAsync(string title, string series = null);
        Task<IEnumerable<string>> GetSeriesAsync();
    }
}