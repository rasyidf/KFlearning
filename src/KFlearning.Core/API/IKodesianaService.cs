// 
//  PROJECT  :   KFlearning
//  FILENAME :   IKodesianaService.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface IKodesianaService
    {
        Task<bool> IsOnline();
        Task<PackageConfig> GetPackageCatalog(PackagePlatform platform);
        Task<IEnumerable<Post>> GetPostsAsync(string series = null);
        Task<IEnumerable<Post>> FindPostAsync(string title, string series = null);
        Task<IEnumerable<string>> GetSeriesAsync();
    }
}