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
    public enum PathKind
    {
        InstallRoot,
        ExtensionRoot,
        LaragonWww,
        Temp,

        ProjectTemplatesJson,
        
        VscodeZip,
        MinGwZip,
        FreeglutZip
    }

    public interface IPathManager
    {
        string GetPath(PathKind kind);
        string StripInvalidFileName(string path);

        void AddVariable(string path);
        void RemoveVariable(string path);
    }

    public class PathManager : IPathManager
    {
        #region Fields

        private const string PathEnv = "path";
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        
        #endregion


        #region Public Methods

        public string GetPath(PathKind kind)
        {
            var rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
            var dataPath = Path.Combine(rootDrive, @"kflearning\data");
            switch (kind)
            {
                case PathKind.InstallRoot:
                    return Path.Combine(rootDrive, @"kflearning");
                case PathKind.LaragonWww:
                    return Path.Combine(rootDrive, @"laragon\www");
                case PathKind.ProjectTemplatesJson:
                    return Path.Combine(rootDrive, @"kflearning\json\project-templates.json");
                case PathKind.VscodeZip:
                {
                    string c = Environment.Is64BitOperatingSystem ? "VSCode-win32-x64-*" : "VSCode-win32-ia32-*";
                    return Path.Combine(rootDrive, FindFile(dataPath, c));
                }
                case PathKind.MinGwZip:
                    return Path.Combine(rootDrive, FindFile(dataPath, "mingw*"));
                case PathKind.FreeglutZip:
                    return Path.Combine(rootDrive, FindFile(dataPath, "freeglut*"));
                case PathKind.Temp:
                {
                    string name = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                    string path = Path.Combine(Path.GetTempPath(), name);
                    Directory.CreateDirectory(path);
                    return path;
                }
                case PathKind.ExtensionRoot:
                    return Path.Combine(rootDrive, Path.Combine(dataPath, @"vscode-exts"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }

            string FindFile(string searchPath, string pattern)
            {
                return Directory.EnumerateFiles(searchPath, pattern).First();
            }
        }

        public string StripInvalidFileName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x.ToString(), string.Empty));
        }
        
        public void AddVariable(string path)
        {
            var parts = GetEnvironmentPath();
            if (parts == null)
            {
                SetEnvironmentPath(new[] {path});
            }
            else
            {
                if (parts.Contains(path)) return;

                parts.Add(path);
                SetEnvironmentPath(parts);
            }
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