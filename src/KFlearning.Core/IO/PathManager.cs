using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace KFlearning.Core.IO
{
    public class PathManager : IPathManager
    {
        #region Fields
        
        private const string EnvironmentVariablePath = "path";

        private Dictionary<PathKind, string> _cachedPaths;
        private static readonly object SyncLock = new object();

        #endregion
        
        #region Public Methods

        public void InitializePaths()
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
                    {PathKind.PathSitesAliasRoot, Path.Combine(basePath, @"etc\apache\sites-enabled")},

                    // app-specific installation dir
                    {PathKind.PathVscodeRoot, Path.Combine(basePath, @"bin\vscode")},
                    {PathKind.PathMingwRoot, Path.Combine(basePath, @"bin\mingw")},
                    {PathKind.PathApacheRoot, Path.Combine(basePath, @"bin\httpd")},
                    {PathKind.PathMariaDbRoot, Path.Combine(basePath, @"bin\mariadb")},
                    {PathKind.PathPhpRoot, Path.Combine(basePath, @"bin\php")},
                    {PathKind.PathKflearningRoot, Path.Combine(basePath, "ide")}
                };

                // app-specific executable paths
                _cachedPaths.Add(PathKind.ExeHttpd, FindFile(_cachedPaths[PathKind.PathApacheRoot], "httpd.exe"));
                _cachedPaths.Add(PathKind.ExeMariadb, FindFile(_cachedPaths[PathKind.PathMariaDbRoot], "mysqld.exe"));
                _cachedPaths.Add(PathKind.ExeVscode, FindFile(_cachedPaths[PathKind.PathVscodeRoot], "Code.exe"));

                // project templates
                var templateRoot = Path.Combine(basePath, @"etc\templates");
                _cachedPaths.Add(PathKind.TemplateWeb, Path.Combine(templateRoot, "web.zip"));
                _cachedPaths.Add(PathKind.TemplateCpp, Path.Combine(templateRoot, "cpp.zip"));
                _cachedPaths.Add(PathKind.TemplatePython, Path.Combine(templateRoot, "python.zip"));
                _cachedPaths.Add(PathKind.TemplateHosts,
                    Path.Combine(systemRoot, @"Windows\System32\drivers\etc\hosts"));
            }
        }

        public string GetPath(PathKind path)
        {
            if (_cachedPaths == null) InitializePaths();
            return _cachedPaths[path];
        }

        public string GetPathForAlias(string domainName)
        {
            return Path.Combine(GetPath(PathKind.PathSitesAliasRoot), domainName + ".conf");
        }

        public string GetPathForTemp(string filename = "")
        {
            return Path.Combine(Path.GetTempPath(), "kflearning", filename);
        }

        public string FindFile(string searchPath, string filename)
        {
            return Directory.EnumerateFiles(searchPath, filename, SearchOption.AllDirectories).FirstOrDefault();
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

        public void LaunchUri(string uri)
        {
            Process.Start(uri);
        }

        public void LaunchExplorer(string path)
        {
            Process.Start("explorer.exe", path);
        }

        public void CreateShortcutOnDesktop(string linkName, string description, string path)
        {
            object shDesktop = "Desktop";
            var shell = new WshShell();
            string shortcutAddress = shell.SpecialFolders.Item(ref shDesktop) + $@"\{linkName}.lnk";
            var shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = description;
            shortcut.TargetPath = path;
            shortcut.Save();
        }

        public void RecursiveDelete(string path)
        {
            RecursiveDelete(path, CancellationToken.None);
        }

        public void RecursiveDelete(string path, CancellationToken token)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    token.ThrowIfCancellationRequested();
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string currentDir in Directory.EnumerateDirectories(path))
                {
                    token.ThrowIfCancellationRequested();
                    RecursiveDelete(currentDir, token);
                }

                Directory.Delete(path);
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void RecursiveMoveDirectory(string source, string destination)
        {
            RecursiveMoveDirectory(source, destination, CancellationToken.None);
        }

        public void RecursiveMoveDirectory(string source, string destination, CancellationToken token)
        {
            Directory.CreateDirectory(destination);
            foreach (string libFile in Directory.EnumerateFiles(source))
            {
                var name = Path.GetFileName(libFile);
                Debug.Assert(name != null);
                File.Move(libFile, Path.Combine(destination, name));
            }

            foreach (string name in Directory.EnumerateDirectories(source).Select(Path.GetFileName))
            {
                var sourceToProcess = Path.Combine(source, name);
                var destToProcess = Path.Combine(destination, name);

                Directory.CreateDirectory(destToProcess);
                RecursiveMoveDirectory(sourceToProcess, destToProcess, token);
            }

            Directory.Delete(source);
        }

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

        #region Private Methods

        private static List<string> GetEnvironmentPath()
        {
            var originalPaths = Environment.GetEnvironmentVariable(EnvironmentVariablePath, EnvironmentVariableTarget.User);
            if (originalPaths == null) return null;
            return new List<string>(originalPaths.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
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

        #endregion
    }
}
