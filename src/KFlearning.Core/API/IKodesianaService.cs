// 
//  PROJECT  :   KFlearning
//  FILENAME :   IKodesianaService.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace KFlearning.Core.API
{
    public interface IKodesianaService
    {
        Task<bool> IsOnline();
        Task<string> GetPostAsync(int postId);
        Task<IEnumerable<string>> GetSeriesAsync();
        Task<PostResponse> GetPostsAsync(int offset, int count);
        Task<PostResponse> GetPostsAsync(int offset, int count, string series);
        Task<PostResponse> FindPostAsync(int offset, int count, string title);
        Task<PostResponse> FindPostAsync(int offset, int count, string title, string series);
    }
}