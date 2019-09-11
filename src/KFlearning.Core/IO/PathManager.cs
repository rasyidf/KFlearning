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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#endregion

namespace KFlearning.Core.IO
{
    public class PathManager : IPathManager
    {
        #region Fields

        private const string PathEnv = "path";
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        
        public string InstallRoot { get; }

        #endregion

        #region Constructor

        public PathManager()
        {
            var rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
            InstallRoot = Path.Combine(rootDrive, "kflearning");
        }

        #endregion

        #region Public Methods

        public string GetModuleInstallPath(ModuleKind module)
        {
            switch (module)
            {
                case ModuleKind.Ide:
                    return Path.Combine(InstallRoot, "ide");
                case ModuleKind.Mingw:
                    return Path.Combine(InstallRoot, @"bin\mingw");
                case ModuleKind.Vscode:
                    return Path.Combine(InstallRoot, @"bin\vscode");
                case ModuleKind.ReposDirectory:
                    return Path.Combine(InstallRoot, "repos");
                default:
                    throw new ArgumentOutOfRangeException(nameof(module), module, null);
            }
        }

        public string StripInvalidFileName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x.ToString(), string.Empty));
        }

        public string EnsureForwardSlash(string path)
        {
            return path.Replace(@"\", "/");
        }

        public string EnsureBackslashEnding(string path)
        {
            bool useForwardSlash = path.Contains("/");
            bool shouldAddSlash = useForwardSlash ? path.EndsWith("/") : path.EndsWith(@"\");

            if (!shouldAddSlash) return path;
            return useForwardSlash ? EnsureForwardSlash(path) + "/" : path + @"\";
        }

        public string GetPathForTemp()
        {
            var name = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var path = Path.Combine(Path.GetTempPath(), name);
            Directory.CreateDirectory(path);

            return path;
        }

        public void LaunchUri(string uri)
        {
            Process.Start(uri);
        }

        public void LaunchExplorer(string path)
        {
            Process.Start("explorer.exe", path);
        }
        
        public void AddVariable(string path)
        {
            var parts = GetEnvironmentPath();
            if (parts == null) return;
            if (parts.Contains(path)) return;

            parts.Add(path);
            SetEnvironmentPath(parts);
        }

        public void RemoveVariable(string path)
        {
            var parts = GetEnvironmentPath();
            if (parts == null) return;
            if (parts.Count(x => x.Contains(path)) == 0) return;

            parts.RemoveAll(x => x.Contains(path));
            SetEnvironmentPath(parts);
        }

        #endregion

        #region Private Methods

        private static List<string> GetEnvironmentPath()
        {
            var originalPaths = Environment.GetEnvironmentVariable(PathEnv, EnvironmentVariableTarget.User);
            if (originalPaths == null) return null;
            return new List<string>(originalPaths.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries));
        }

        private static void SetEnvironmentPath(IEnumerable<string> paths)
        {
            var revisedPath = string.Join(";", paths);
            Environment.SetEnvironmentVariable(PathEnv, revisedPath, EnvironmentVariableTarget.User);
        }

        #endregion
    }
}