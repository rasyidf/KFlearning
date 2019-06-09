using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace KFlearning.ApplicationServices
{
    public class ProcessManager : IProcessManager
    {
        public string GetPath(PathKind path)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            switch (path)
            {
                case PathKind.BasePath:
                    return basePath;
                case PathKind.HostsFile:
                    return Path.Combine(Path.GetPathRoot(Environment.SystemDirectory),
                        @"Windows\System32\drivers\etc\hosts");
                case PathKind.VscodeRoot:
                    return Path.Combine(basePath, "bin\\vscode");
                case PathKind.PhpRoot:
                    return Path.Combine(basePath, "bin\\php");
                case PathKind.ComposerRoot:
                    return Path.Combine(basePath, "etc\\composer");
                case PathKind.MingwBinRoot:
                    return Path.Combine(basePath, "bin\\mingw\\bin");
                case PathKind.ReposRoot:
                    return Path.Combine(basePath, "repos");
                case PathKind.ApacheSitesRoot:
                    return Path.Combine(basePath, "etc\\apache\\sites-enabled");
                case PathKind.VscodeExtensionRoot:
                    return Path.Combine(basePath, "etc\\vscode\\ext");
                case PathKind.VscodeWebZip:
                    return Path.Combine(basePath, "etc\\vscode\\templates\\web.zip");
                case PathKind.VscodeCppZip:
                    return Path.Combine(basePath, "etc\\vscode\\templates\\cpp.zip");
                case PathKind.VscodePythonZip:
                    return Path.Combine(basePath, "etc\\vscode\\templates\\python.zip");
                case PathKind.HttpdExe:
                    return Path.Combine(basePath, "bin\\httpd\\bin\\httpd.exe");
                case PathKind.MariadbExe:
                    return Path.Combine(basePath, "bin\\mariadb\\bin\\mysqld.exe");
                case PathKind.VscodeExe:
                    return Path.Combine(basePath, "bin\\vscode\\code.exe");
                default:
                    throw new ArgumentOutOfRangeException(nameof(path), path, null);
            }
        }

        public string EnsureBackslashEnding(string path)
        {
            var sb = new StringBuilder(path);
            sb.Replace("\\", "/");
            if (sb[sb.Length] != '/') sb.Append("/");
            return sb.ToString();
        }

        public bool IsRunning(string name)
        {
            var processes = Process.GetProcessesByName("httpd");
            return processes.Length > 0;
        }

        public void RunWait(string filename, string args)
        {
            throw new NotImplementedException();
        }

        public void RunJob(string filename, string args)
        {
            throw new NotImplementedException();
        }

        public void TerminateJob(string processName)
        {
            throw new NotImplementedException();
        }
    }
}
