using System;
using System.Collections.Generic;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Sequence
{
    public class KflearningTask : ITaskNode
    {
        public string TaskName => "KFlearning IDE";

        public void Run(InstallDefinition definition, CancellationToken cancellation)
        {
            var progress = definition.ResolveService<IProgressBroker>();
            var fileSystem = definition.ResolveService<IFileSystemManager>();
            var path = definition.ResolveService<IPathManager>();
            
            // find zip and extract
            progress.ReportMessage("Extracting KFlearning...");
            var apacheZip = fileSystem.FindFile(definition.DataPath, "kflearning-ide-*");
            using (var extractor = new ZipExtractor((s, e) => progress.ReportNodeProgress(e.ProgressPercentage)))
            {
                extractor.ExtractAll(apacheZip, path.GetPath(PathKind.PathKflearningRoot));
            }

            // TODO: save content
            progress.ReportMessage("Creating default hosts...");

            // add default site alias
            progress.ReportMessage("Creating HTTPD alias...");
            var indexPath = path.EnsureBackslashEnding(path.EnsureForwardSlash(
                path.Combine(PathKind.PathBase, @"etc\kflearning")));
            var defaultAliasPath = path.Combine(PathKind.PathBase, @"etc\apache\alias\0-default.conf");
            using (var alias = new TransformingConfigFile(defaultAliasPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "kflearning");
                alias.Transform("{ALIAS_PATH}", indexPath);
            }

            // add default site virtual host
            progress.ReportMessage("Creating HTTPD virtual host...");
            var defaultHostPath =path.Combine(PathKind.PathBase, @"etc\apache\sites-enabled\0-default.conf");
            using (var config = new TransformingConfigFile(defaultHostPath, Constants.DefaultVirtualHost))
            {
                config.Transform("{KFLEARNING_DIR_ROOT}", indexPath);
            }

            // copy templates
            var templatePatterns = new List<Tuple<string, PathKind>>
            {
                new Tuple<string, PathKind>("template-cpp*", PathKind.TemplateCpp),
                new Tuple<string, PathKind>("template-web*", PathKind.TemplateWeb),
                new Tuple<string, PathKind>("template-python*", PathKind.TemplatePython),
            };

            progress.ReportMessage("Copying templates...");
            for (int i = 0; i < templatePatterns.Count; i++)
            {
                var item = templatePatterns[i];
                fileSystem.CopyFile(fileSystem.FindFile(definition.DataPath, item.Item1), path.GetPath(item.Item2));
                progress.ReportNodeProgress(MathHelper.CalculatePercentage(i + 1, templatePatterns.Count));
            }

            // create shortcut
            progress.ReportNodeProgress(-1);
            progress.ReportMessage("Creating desktop shortcut...");
            var exePath = path.Combine(PathKind.PathKflearningRoot, "KFlearning.IDE.exe");
            fileSystem.CreateShortcutOnDesktop("KFlearning", "KFlearning", exePath);
        }
    }
}
