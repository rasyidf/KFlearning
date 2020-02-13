// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : PathManager.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

#region

using System;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

namespace KFlearning.Core.IO
{
    public interface IPathManager
    {
        string GetPath(PathKind kind, bool forwardSlash = false);
        string StripInvalidPathName(string path);
    }

    public class PathManager : IPathManager
    {
        private static readonly string InstallRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        private string _cachedVscodePath;

        public string GetPath(PathKind kind, bool forwardSlash = false)
        {
            string path;
            switch (kind)
            {
                case PathKind.DefaultProjectRoot:
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "KFlearning");
                    break;
                case PathKind.PersistanceDirectory:
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        @"KFlearning\settings");
                    break;
                case PathKind.WallpaperPath:
                    path = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "wallpaper.jpg");
                    break;
                case PathKind.VisualStudioCodeExecutable:
                    path = _cachedVscodePath ?? FindVscode();
                    _cachedVscodePath = path;
                    break;
                case PathKind.MingwInclude1Directory:
                    path = Path.Combine(InstallRoot, @"mingw32\include");
                    break;
                case PathKind.MingwInclude2Directory:
                    path = Path.Combine(InstallRoot, @"mingw32\i686-w64-mingw32\include");
                    break;
                case PathKind.MingwGXXExecutable:
                    path = Path.Combine(InstallRoot, @"mingw32\bin\g++.exe");
                    break;
                case PathKind.MingwGDBExecutable:
                    path = Path.Combine(InstallRoot, @"mingw32\bin\gdb.exe");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }

            return forwardSlash ? path.Replace('\\', '/') : path;
        }

        public string StripInvalidPathName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x, '_'));
        }

        private string FindVscode()
        {
            // find in env path
            var userEnv = Environment.GetEnvironmentVariable("path");
            var path = userEnv?.Split(Path.PathSeparator).FirstOrDefault(x => x.Contains("Microsoft VS Code"));
            if (path != null) return Path.Combine(path.Substring(0, path.Length - 4), "code.exe");

            // find in user dir
            var userDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userInstall = Path.Combine(userDir, @"Programs\Microsoft VS Code\code.exe");
            if (File.Exists(userInstall)) return userInstall;

            return null;
        }
    }
}