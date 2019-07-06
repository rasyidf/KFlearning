using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace KFlearning.Core.IO
{
    public class PathManager : IPathManager
    {
        private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private string _httpd, _mariadb, _vscode;

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(path), path, null);
            }
        }

        public string GetPath(ExecutableFile file)
        {
            switch (file)
            {
                case ExecutableFile.Httpd:
                    return _httpd ?? (_httpd = FindEnumerate(Path.Combine(BasePath, @"bin\httpd"), "httpd.exe"));
                case ExecutableFile.Mariadb:
                    return _mariadb ?? (_mariadb = FindEnumerate(Path.Combine(BasePath, @"bin\mariadb"), "mysqld.exe"));
                case ExecutableFile.Vscode:
                    return _vscode ?? (_vscode = FindEnumerate(Path.Combine(BasePath, @"bin\vscode"), "Code.exe"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(file), file, null);
            }
        }

        public string GetPath(TemplateFile file)
        {
            switch (file)
            {
                case TemplateFile.Hosts:
                    return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory),
                        @"Windows\System32\drivers\etc\hosts");
                case TemplateFile.Web:
                    return Path.Combine(BasePath, @"etc\vscode\templates\web.zip");
                case TemplateFile.Cpp:
                    return Path.Combine(BasePath, @"etc\vscode\templates\cpp.zip");
                case TemplateFile.Python:
                    return Path.Combine(BasePath, @"etc\vscode\templates\python.zip");
                default:
                    throw new ArgumentOutOfRangeException(nameof(file), file, null);
            }
        }

        public string GetPathForAlias(string domainName)
        {
            return Path.Combine(GetPath(PathKind.ApacheSitesAliasRoot), domainName + ".conf");
        }

        public void LaunchUri(string uri)
        {
            Process.Start(uri);
        }

        public void LaunchExplorer(string path)
        {
            Process.Start("explorer.exe", path);
        }

        public string EnsureBackslashEnding(string path)
        {
            var sb = new StringBuilder(path);
            sb.Replace("\\", "/");
            if (sb[sb.Length - 1] != '/') sb.Append("/");
            return sb.ToString();
        }
        
        private static string FindEnumerate(string searchPath, string filename)
        {
            return Directory.EnumerateFiles(searchPath, filename, SearchOption.AllDirectories).FirstOrDefault();
        }
    }
}
