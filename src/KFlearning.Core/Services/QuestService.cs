// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : QuestService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.IO;
using System.Linq;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services
{
    public interface IQuestService : IUsesPersistance
    {
        QuestStatistics GetStatistics();
        void ChangeScores(long codeCount, TimeSpan codeTime, int projectCount);
        void UpdateScores(bool restart = true);
    }

    public class QuestService : IQuestService
    {
        private const string QuestSettingsKey = "Quest.Settings";

        private readonly IPersistanceStorage _storage;
        private readonly IProcessWatcher _watcher;
        private readonly IPathManager _path;
        private QuestSettings _settings;

        public QuestService(IPersistanceStorage storage, IProcessWatcher watcher, IPathManager path)
        {
            _storage = storage;
            _watcher = watcher;
            _path = path;
            Load();
        }

        public QuestStatistics GetStatistics()
        {
            return new QuestStatistics(_settings);
        }

        public void ChangeScores(long codeCount, TimeSpan codeTime, int projectCount)
        {
            _settings.CodeCount = codeCount;
            _settings.CodingTime = codeTime;
            _settings.ProjectCount = projectCount;
            _storage.Store(QuestSettingsKey, _settings);
        }

        public void UpdateScores(bool restart = true)
        {
            _watcher.Stop();

            var projectPath = _path.GetPath(PathKind.DefaultProjectRoot);
            var files = Directory.EnumerateFiles(projectPath, "*", SearchOption.AllDirectories);
            _settings.CodeCount = files.AsParallel().Select(Helpers.CountLines).Sum();
            _settings.CodingTime = _settings.CodingTime.Add(TimeSpan.FromSeconds(_watcher.TotalSeconds));
            _settings.ProjectCount = Directory.EnumerateDirectories(projectPath).AsParallel().Count() - 1;

            if (restart) _watcher.Start();
        }

        public void Load()
        {
            _settings = _storage.Retrieve<QuestSettings>(QuestSettingsKey) ?? CreateDefaultSettings();
            _watcher.ProcessName = "code";
            _watcher.Start();
        }

        public void Save()
        {
            UpdateScores(false);
            _storage.Store(QuestSettingsKey, _settings);
        }

        private QuestSettings CreateDefaultSettings()
        {
            return new QuestSettings
            {
                CodeCount = 0,
                ProjectCount = 0,
                CodingTime = TimeSpan.Zero
            };
        }
    }
}