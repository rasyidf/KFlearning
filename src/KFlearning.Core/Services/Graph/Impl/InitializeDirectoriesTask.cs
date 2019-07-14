using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class InitializeDirectoriesTask : ITaskNode
    {
        private readonly bool _install;

        public string TaskName => "Initialize Directories";

        public InitializeDirectoriesTask(bool install)
        {
            _install = install;
        }

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            var baseDir = path.GetPath(PathKind.PathBase);

            if (_install)
            {
                Directory.CreateDirectory(baseDir);
                Directory.CreateDirectory(Path.Combine(baseDir, "bin"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\httpd"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\mariadb"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\mingw"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\php"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\composer"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"bin\vscode"));

                Directory.CreateDirectory(Path.Combine(baseDir, "etc"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache\alias"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache\sites-enabled"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\kflearning"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\phpMyAdmin"));
                Directory.CreateDirectory(Path.Combine(baseDir, @"etc\templates"));

                Directory.CreateDirectory(Path.Combine(baseDir, "ide"));
                Directory.CreateDirectory(Path.Combine(baseDir, "repos"));
            }
            else
            {
                path.RecursiveDelete(Path.Combine(baseDir, "bin"), cancellation);
                path.RecursiveDelete(Path.Combine(baseDir, "etc"), cancellation);
                path.RecursiveDelete(Path.Combine(baseDir, "ide"), cancellation);
            }
        }
    }
}
