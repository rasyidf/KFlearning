// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : UserService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Threading.Tasks;
using KFlearning.Core.API;
using KFlearning.Core.API.Model;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services
{
    public interface IUserService : IUsesPersistance
    {
        bool IsLogged { get; }
        string Username { get; }
        DateTime LastSync { get; }

        Task Login(string token);
        void Logout();
        Task Sync();
    }

    public class UserService : IUserService
    {
        private readonly IPersistanceStorage _storage;
        private readonly ILeaderboardService _leaderboard;
        private readonly IQuestService _quest;
        private UserSettings _settings;

        private const string UserSettingsKey = "User.Settings";

        public bool IsLogged => _settings.Username != "Belum login";
        public string Username => _settings.Username;
        public DateTime LastSync => _settings.LastSync;

        public UserService(ILeaderboardService leaderboard, IQuestService quest, IPersistanceStorage storage)
        {
            _leaderboard = leaderboard;
            _quest = quest;
            _storage = storage;

            Load();
        }

        public async Task Login(string token)
        {
            var username = await _leaderboard.GetUsername(token);
            _settings.Username = username;
            _settings.LastSync = DateTime.Now;

            var stats = await _leaderboard.GetStatistics(username);
            if (stats.Score1 == 0 && stats.Score2 == 0 && stats.Score3 == 0)
            {
                _quest.UpdateScores();
                var savedStats = _quest.GetStatistics();
                var newStats = new UserProfile
                {
                    Username = username,
                    Score1 = savedStats.CodeCount,
                    Score2 = savedStats.CodingTime.TotalSeconds,
                    Score3 = savedStats.ProjectCount
                };

                await _leaderboard.UpdateStatistics(newStats);
            }
            else
            {
                _quest.ChangeScores((long) stats.Score1, TimeSpan.FromSeconds(stats.Score2), (int) stats.Score3);
            }
        }

        public void Logout()
        {
            _settings.Username = "Belum login";
            _settings.LastSync = DateTime.Now;
        }

        public async Task Sync()
        {
            var savedStats = _quest.GetStatistics();
            var newStats = new UserProfile
            {
                Username = Username,
                Score1 = savedStats.CodeCount,
                Score2 = savedStats.CodingTime.TotalSeconds,
                Score3 = savedStats.ProjectCount
            };
            await _leaderboard.UpdateStatistics(newStats);
        }

        public void Load()
        {
            _settings = _storage.Retrieve<UserSettings>(UserSettingsKey) ?? new UserSettings
                            {LastSync = DateTime.Now, Username = "Belum login"};
        }

        public void Save()
        {
            _storage.Store(UserSettingsKey, _settings);
        }
    }
}