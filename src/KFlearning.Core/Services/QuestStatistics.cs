// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : QuestStatistics.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Linq;
using KFlearning.Core.Resources;

namespace KFlearning.Core.Services
{
    public class QuestStatistics
    {
        private readonly QuestSettings _settings;

        public QuestStatistics(QuestSettings settings)
        {
            _settings = settings;
        }

        public long CodeCount => _settings.CodeCount;

        public TimeSpan CodingTime => _settings.CodingTime;

        public int ProjectCount => _settings.ProjectCount;

        public int CoderLevel => Math.Min((int) _settings.CodeCount / 500, 3);

        public string CoderDescription
        {
            get
            {
                switch (CoderLevel)
                {
                    case 1: return Strings.QuestCoderLevel2;
                    case 2: return Strings.QuestCoderLevel3;
                    case 3: return Strings.QuestCoderComplete;
                    default: return Strings.QuestCoderLevel1;
                }
            }
        }

        public int FocusLevel => Math.Min((int) _settings.CodingTime.TotalHours / 2, 3);

        public string FocusDescription
        {
            get
            {
                switch (FocusLevel)
                {
                    case 1: return Strings.QuestFocusLevel2;
                    case 2: return Strings.QuestFocusLevel3;
                    case 3: return Strings.QuestFocusComplete;
                    default: return Strings.QuestFocusLevel1;
                }
            }
        }

        public int ProjectLevel => Math.Min(_settings.ProjectCount / 5, 3);

        public string ProjectDescription
        {
            get
            {
                switch (ProjectLevel)
                {
                    case 1: return Strings.QuestProjectLevel2;
                    case 2: return Strings.QuestProjectLevel3;
                    case 3: return Strings.QuestProjectComplete;
                    default: return Strings.QuestProjectLevel1;
                }
            }
        }

        public static string LevelToString(int level)
        {
            return string.Concat(Enumerable.Repeat(Strings.StarFilled, level)) +
                   string.Concat(Enumerable.Repeat(Strings.StarOutline, 3 - level));
        }
    }
}