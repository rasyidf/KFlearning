using System.IO;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class PhpMyAdminTask : ITaskNode
    {
        public string TaskName => "phpMyAdmin";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();

            // install phpmyadmin
            var phpAdminSourcePath = Directory
                .EnumerateDirectories(path.GetPathForTemp(), "phpMyAdmin*", SearchOption.TopDirectoryOnly)
                .First();
            var phpAdminDestPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\phpmyadmin");
            path.RecursiveMoveDirectory(phpAdminSourcePath, phpAdminDestPath, cancellation);

            // add phpmyadmin to alias
            var configPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\apache\alias\phpmyadmin.conf");
            using (var alias = new TransformingConfigFile(configPath, Constants.AliasTemplate))
            {
                var phpAdminPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\phpmyadmin");
                alias.Transform("{ALIAS_NAME}", "phpmyadmin");
                alias.Transform("{ALIAS_PATH}",
                    path.EnsureBackslashEnding(path.EnsureForwardSlash(phpAdminPath)));
            }
        }
    }
}
