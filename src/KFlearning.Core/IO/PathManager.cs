// 
//  PROJECT  :   KFlearning
//  FILENAME :   PathManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Linq;

#endregion

namespace KFlearning.Core.IO
{
    public interface IPathManager
    {
        string GetPath(PathKind kind);
        bool DiscoverVisualStudioCode(out string installRoot);
        string StripInvalidPathName(string path);
    }

    public class PathManager : IPathManager
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

        public string GetPath(PathKind kind)
        {
            switch (kind)
            {
                case PathKind.DefaultProjectRoot:
                    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "KFlearning");
                case PathKind.HistoryFile:
                    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        @"KFlearning\history.json");
                case PathKind.WallpaperPath:
                    return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "wallpaper.jpg");
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        public bool DiscoverVisualStudioCode(out string installRoot)
        {
            var userDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userInstall = Path.Combine(userDir, @"Programs\Microsoft VS Code\code.exe");

            installRoot = userInstall;
            return File.Exists(userInstall);
        }

        public string StripInvalidPathName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x, '_'));
        }
    }
}