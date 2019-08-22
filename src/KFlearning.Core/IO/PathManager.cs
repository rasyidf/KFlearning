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

        private const string EnvironmentVariablePath = "path";

        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        private static readonly object SyncLock = new object();

        private Dictionary<PathKind, string> _cachedPaths;

        #endregion

        #region Public Methods

        #region Path Manipulations

        public string Combine(PathKind path, params string[] parts)
        {
            var aggregate = new List<string>();
            aggregate.Add(GetPath(path));
            aggregate.AddRange(parts);
            return Combine(aggregate.ToArray());
        }

        public string Combine(params string[] parts)
        {
            return Path.Combine(parts);
        }

        public string GetPath(PathKind path)
        {
            if (_cachedPaths == null) InitializePaths();
            return _cachedPaths[path];
        }

        public string GetPathForVirtualHost(string domainName)
        {
            return Path.Combine(GetPath(PathKind.PathVirtualHostRoot), domainName + ".conf");
        }

        public string GetPathForAlias(string domainName)
        {
            return Path.Combine(GetPath(PathKind.PathSitesAliasRoot), domainName + ".conf");
        }

        public string GetPathForTemp(string filename = "")
        {
            return Path.Combine(Path.GetTempPath(), "kflearning", filename);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
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

        #endregion

        #region Shell Operations

        public void LaunchUri(string uri)
        {
            Process.Start(uri);
        }

        public void LaunchExplorer(string path)
        {
            Process.Start("explorer.exe", path);
        }

        #endregion

        #region Environment Paths

        public void AddPathEnvironmentVar(string path)
        {
            var parts = GetEnvironmentPath();
            if (parts == null) return;
            if (parts.Contains(path)) return;

            parts.Add(path);
            SetEnvironmentPath(parts);
        }

        public void RemovePathEnvironmentVar(string path)
        {
            var parts = GetEnvironmentPath();
            if (parts == null) return;
            if (parts.Count(x => x.Contains(path)) == 0) return;

            parts.RemoveAll(x => x.Contains(path));
            SetEnvironmentPath(parts);
        }

        #endregion

        #endregion

        #region Private Methods

        private static List<string> GetEnvironmentPath()
        {
            var originalPaths =
                Environment.GetEnvironmentVariable(EnvironmentVariablePath, EnvironmentVariableTarget.User);
            if (originalPaths == null) return null;
            return new List<string>(originalPaths.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries));
        }

        private static void SetEnvironmentPath(IEnumerable<string> paths)
        {
            var revisedPath = string.Join(";", paths);
            Environment.SetEnvironmentVariable(EnvironmentVariablePath, revisedPath, EnvironmentVariableTarget.User);
        }

        private static string GetBasePath()
        {
            var basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            Debug.Assert(basePath != null);
            int lastIndex = basePath.LastIndexOf(@"\", StringComparison.InvariantCultureIgnoreCase);
            return basePath.Substring(0, lastIndex);
        }

        private void InitializePaths()
        {
            lock (SyncLock)
            {
                // find base path, relative one level up from this executable
                string basePath = GetBasePath();
                string systemRoot = Path.GetPathRoot(Environment.SystemDirectory);

                // cache paths
                _cachedPaths = new Dictionary<PathKind, string>
                {
                    // common paths
                    {PathKind.PathBase, basePath},
                    {PathKind.PathReposRoot, Path.Combine(basePath, "repos")},
                    {PathKind.PathSitesAliasRoot, Path.Combine(basePath, @"etc\apache\alias")},
                    {PathKind.PathVirtualHostRoot, Path.Combine(basePath, @"etc\apache\sites-enabled")},

                    // app-specific installation dir
                    {PathKind.PathVscodeRoot, Path.Combine(basePath, @"bin\vscode")},
                    {PathKind.PathMingwRoot, Path.Combine(basePath, @"bin\mingw")},
                    {PathKind.PathApacheRoot, Path.Combine(basePath, @"bin\httpd")},
                    {PathKind.PathMariaDbRoot, Path.Combine(basePath, @"bin\mariadb")},
                    {PathKind.PathPhpRoot, Path.Combine(basePath, @"bin\php")},
                    {PathKind.PathComposerRoot, Path.Combine(basePath, @"bin\composer")},
                    {PathKind.PathKflearningRoot, Path.Combine(basePath, "ide")}
                };

                // app-specific executable paths
                _cachedPaths.Add(PathKind.ExeHttpd, Path.Combine(_cachedPaths[PathKind.PathApacheRoot], "httpd.exe"));
                _cachedPaths.Add(PathKind.ExeMariadb,
                    Path.Combine(_cachedPaths[PathKind.PathMariaDbRoot], "mysqld.exe"));
                _cachedPaths.Add(PathKind.ExeVscode, Path.Combine(_cachedPaths[PathKind.PathVscodeRoot], "Code.exe"));

                // project templates
                var templateRoot = Path.Combine(basePath, @"etc\templates");
                _cachedPaths.Add(PathKind.TemplateHosts,
                    Path.Combine(systemRoot, @"Windows\System32\drivers\etc\hosts"));
            }
        }

        #endregion
    }
}