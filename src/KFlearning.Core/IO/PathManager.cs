using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace KFlearning.Core.IO
{
    public class PathManager : IPathManager
    {
        #region Fields
        
        private const string EnvironmentVariablePath = "path";

        private static readonly string BasePath;
        private string _httpd, _mariadb, _vscode; 

        #endregion

        #region Static Constructor

        static PathManager()
        {
            var basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            Debug.Assert(basePath != null);
            var lastIndex = basePath.LastIndexOf(@"\", StringComparison.InvariantCultureIgnoreCase);
            BasePath = basePath.Substring(0, basePath.Length - (basePath.Length - lastIndex));
        }

        #endregion

        #region Public Methods

        public string GetPath(PathKind path)
        {
            switch (path)
            {
                case PathKind.BasePath:
                    return BasePath;
                case PathKind.ReposRoot:
                    return Path.Combine(BasePath, "repos");
                case PathKind.ApacheSitesAliasRoot:
                    return Path.Combine(BasePath, @"etc\apache\sites-enabled");
                case PathKind.VscodeInstallRoot:
                    return Path.Combine(BasePath, @"bin\vscode");
                case PathKind.MingwInstallRoot:
                    return Path.Combine(BasePath, @"bin\mingw");
                case PathKind.ApacheInstallRoot:
                    return Path.Combine(BasePath, @"bin\httpd");
                case PathKind.MariaDbInstallRoot:
                    return Path.Combine(BasePath, @"bin\mariadb");
                case PathKind.PhpInstallRoot:
                    return Path.Combine(BasePath, @"bin\php");
                default:
                    throw new ArgumentOutOfRangeException(nameof(path), path, null);
            }
        }

        public string GetPath(ExecutableFile file)
        {
            switch (file)
            {
                case ExecutableFile.Httpd:
                    return _httpd ?? (_httpd = FindEnumerate(GetPath(PathKind.ApacheInstallRoot), "httpd.exe"));
                case ExecutableFile.Mariadb:
                    return _mariadb ?? (_mariadb = FindEnumerate(GetPath(PathKind.MariaDbInstallRoot), "mysqld.exe"));
                case ExecutableFile.Vscode:
                    return _vscode ?? (_vscode = FindEnumerate(GetPath(PathKind.VscodeInstallRoot), "Code.exe"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(file), file, null);
            }
        }

        public string GetPath(TemplateFile file)
        {
            switch (file)
            {
                case TemplateFile.RootDir:
                    return Path.Combine(BasePath, @"etc\vscode\templates");
                case TemplateFile.Hosts:
                    return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory),
                        @"Windows\System32\drivers\etc\hosts");
                case TemplateFile.Web:
                    return Path.Combine(GetPath(TemplateFile.RootDir), "web.zip");
                case TemplateFile.Cpp:
                    return Path.Combine(GetPath(TemplateFile.RootDir), "cpp.zip");
                case TemplateFile.Python:
                    return Path.Combine(GetPath(TemplateFile.RootDir), "python.zip");
                default:
                    throw new ArgumentOutOfRangeException(nameof(file), file, null);
            }
        }

        public string GetPathForAlias(string domainName)
        {
            return Path.Combine(GetPath(PathKind.ApacheSitesAliasRoot), domainName + ".conf");
        }

        public string GetPathForTemp(string filename)
        {
            return Path.Combine(Path.GetTempPath(), "kflearning", filename);
        }

        public string EnsureBackslashEnding(string path)
        {
            var sb = new StringBuilder(path);
            sb.Replace("\\", "/");
            if (sb[sb.Length - 1] != '/') sb.Append("/");
            return sb.ToString();
        }

        public void LaunchUri(string uri)
        {
            Process.Start(uri);
        }

        public void LaunchExplorer(string path)
        {
            Process.Start("explorer.exe", path);
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

        private static string FindEnumerate(string searchPath, string filename)
        {
            return Directory.EnumerateFiles(searchPath, filename, SearchOption.AllDirectories).FirstOrDefault();
        }

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

        #endregion
    }
}
