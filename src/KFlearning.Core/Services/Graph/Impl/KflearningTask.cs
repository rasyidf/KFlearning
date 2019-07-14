using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class KflearningTask : ITaskNode
    {
        public string TaskName => "KFlearning IDE";

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            var path = definition.ResolveService<IPathManager>();
            
            // find zip and extract
            var apacheZip = path.FindFile(definition.DataPath, "kflearning-ide*");
            var extractor = new ZipExtractor();
            extractor.ExtractAll(apacheZip, path.GetPath(PathKind.PathKflearningRoot));

            // create shortcut
            var exePath = Path.Combine(path.GetPath(PathKind.PathKflearningRoot), "KFlearning.IDE.exe");
            path.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);

            // save content
            // TODO: save content

            // add default site alias
            var indexPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\kflearning");
            indexPath = path.EnsureBackslashEnding(path.EnsureForwardSlash(indexPath));
            var defaultAliasPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\apache\alias\0-default.conf");
            using (var alias = new TransformingConfigFile(defaultAliasPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "kflearning");
                alias.Transform("{ALIAS_PATH}", indexPath);
            }

            // add default site virtual host
            var defaultHostPath = Path.Combine(path.GetPath(PathKind.PathBase), @"etc\apache\sites-enabled\0-default.conf");
            using (var config = new TransformingConfigFile(defaultHostPath, Constants.DefaultVirtualHost))
            {
                config.Transform("{KFLEARNING_DIR_ROOT}", indexPath);
            }

            // copy templates
            var templatePatterns = new List<Tuple<string, PathKind>>
            {
                new Tuple<string, PathKind>("template-cpp*", PathKind.TemplateCpp),
                new Tuple<string, PathKind>("template-web*", PathKind.TemplateCpp),
                new Tuple<string, PathKind>("template-python*", PathKind.TemplateCpp),
            };

            for (int i = 0; i < templatePatterns.Count; i++)
            {
                var item = templatePatterns[i];
                File.Copy(path.FindFile(definition.DataPath, item.Item1), path.GetPath(item.Item2));
            }
        }
    }
}
