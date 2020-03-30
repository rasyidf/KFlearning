// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : LeaderboardService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Threading.Tasks;
using KFlearning.Core.API.Model;
using KFlearning.Core.Security;

namespace KFlearning.Core.API
{
    public interface ILeaderboardService
    {
        Task<string> GetUsername(string token);
        Task<UserProfile> GetStatistics(string username);
        Task UpdateStatistics(UserProfile profile);
    }

    public class LeaderboardService : ApiServiceBase, ILeaderboardService
    {
        public LeaderboardService(IAuthorizationService authorizationService) : base(authorizationService)
        {
        }

        public async Task<string> GetUsername(string token)
        {
            var body = new ApiRequestBody
            {
                Action = "get_username",
                User = new UserProfile
                {
                    Username = token
                }
            };

            var response = await GetResponse(body);
            if (!response.Success) throw new KFlearningException(response.Reason);
            return response.User.Username;
        }

        public async Task<UserProfile> GetStatistics(string username)
        {
            var body = new ApiRequestBody
            {
                Action = "get_stats",
                User = new UserProfile
                {
                    Username = username
                }
            };

            var response = await GetResponse(body);
            if (!response.Success) throw new KFlearningException(response.Reason);
            return response.User;
        }

        public async Task UpdateStatistics(UserProfile profile)
        {
            var body = new ApiRequestBody
            {
                Action = "update_stats",
                User = profile
            };

            var response = await GetResponse(body);
            if (!response.Success) throw new KFlearningException(response.Reason);
        }
    }
}