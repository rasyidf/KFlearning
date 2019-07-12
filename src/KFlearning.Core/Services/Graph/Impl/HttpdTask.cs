using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class HttpdTask : ITaskNode
    {
           #region Fields

        private IProgressBroker _broker;
        private IPathManager _pathManager;
        private InstallMode _mode;

        #endregion

        #region ITaskNode Properties

        public string TaskName => "Apache HTTPD";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; } = new Queue<ITaskNode>();

        #endregion

        #region ITaskNode Methods

        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;

            if (definition.Mode == InstallMode.Install)
            {
                // httpd
                var fileName = Path.GetFileName(definition.Packages.ApacheUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.ApacheUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.PathApacheRoot)));

                // phpmyadmin
                fileName = Path.GetFileName(definition.Packages.PhpmyadminUri.AbsoluteUri);
                savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.PhpmyadminUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPathForTemp()));
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(_pathManager.GetPath(PathKind.PathPhpRoot)));
            }
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportProgress(-1);
                if (_mode == InstallMode.Install)
                {
                    _broker.ReportMessage("Installing Apache HTTPD...");
                    InternalInstall();
                }
                else
                {
                    _broker.ReportMessage("Uninstalling Apache HTTPD...");
                    InternalUninstall();
                }

                return true;
            }
            catch (Exception e)
            {
                _broker.ReportProgress(100);
                _broker.ReportMessage(e.ToString());
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void InternalInstall()
        {
            // invalidate caches
            _pathManager.InitializePaths();

            // find root directory
            var root = _pathManager.GetPath(PathKind.PathApacheRoot);
            var rootNested = Directory.EnumerateDirectories(root, "*", SearchOption.TopDirectoryOnly).First();
            _pathManager.RecursiveMoveDirectory(rootNested, root);

            // save settings (httpd.conf)
            _broker.ReportMessage("Configuring Apache...");
            using (var config = new TransformingConfigFile(Path.Combine(root, @"conf\php.ini"), Constants.HttpdConfig))
            {
                config.Transform("{HTTPD_ROOT}", _pathManager.EnsureForwardSlash(root));
                config.Transform("{DOCUMENT_ROOT}",
                    _pathManager.EnsureForwardSlash(_pathManager.GetPath(PathKind.PathReposRoot)));
                config.Transform("{ALIAS_PATH}",
                    _pathManager.EnsureForwardSlash(_pathManager.GetPath(PathKind.PathSitesAliasRoot)));
                config.Transform("{SITES_PATH}",
                    _pathManager.EnsureForwardSlash(_pathManager.GetPath(PathKind.PathVirtualHostRoot)));
                config.Transform("{PHP_PATH}",
                    _pathManager.EnsureForwardSlash(_pathManager.GetPath(PathKind.PathPhpRoot)));
                var phpModule = _pathManager.EnsureForwardSlash(Path.Combine(_pathManager.GetPath(PathKind.PathPhpRoot),
                    "php7apache2_4.dll"));
                config.Transform("{PHP_MODULE_PATH}", phpModule);
            }

            // install phpmyadmin
            _broker.ReportMessage("Installing phpMyAdmin...");
            var phpAdminSourcePath = Directory
                .EnumerateDirectories(_pathManager.GetPathForTemp(), "phpMyAdmin*", SearchOption.TopDirectoryOnly)
                .First();
            var phpAdminDestPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\phpmyadmin");
            _pathManager.RecursiveMoveDirectory(phpAdminSourcePath, phpAdminDestPath);

            // add phpmyadmin to sites
            var phpAdminPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\apache\alias\phpmyadmin.conf");
            using (var alias = new TransformingConfigFile(phpAdminPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "phpmyadmin");
                alias.Transform("{ALIAS_PATH}",
                    _pathManager.EnsureBackslashEnding(_pathManager.EnsureForwardSlash(phpAdminDestPath)));
            }

            // add default site alias
            var indexPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\kflearning");
            indexPath = _pathManager.EnsureBackslashEnding(_pathManager.EnsureForwardSlash(indexPath));
            var defaultAliasPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\apache\alias\0-default.conf");
            using (var alias = new TransformingConfigFile(defaultAliasPath, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_NAME}", "kflearning");
                alias.Transform("{ALIAS_PATH}", indexPath);
            }

            // add default site virtual host
            var defaultHostPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\apache\sites-enabled\0-default.conf");
            using (var config = new TransformingConfigFile(defaultHostPath, Constants.DefaultVirtualHost))
            {
                config.Transform("{KFLEARNING_DIR_ROOT}", indexPath);
            }

            // add to env path
            _broker.ReportMessage("Adding Apache HTTPD to environment variable...");
            _pathManager.AddPathEnvironmentVar(root);
        }

        private void InternalUninstall()
        {
            // remove from env
            _broker.ReportMessage("Removing Apache HTTPD from environment variable...");
            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.PathPhpRoot));
        }

        #endregion
    }
}
