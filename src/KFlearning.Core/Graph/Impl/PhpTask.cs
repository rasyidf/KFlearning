using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class PhpTask : ITaskNode
    {
         #region Fields
        
        private IProgressBroker _broker;
        private IPathManager _pathManager;
        private InstallMode _mode;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "PHP Interpreter";
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
                // php
                var fileName = Path.GetFileName(definition.Packages.PhpUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.PhpUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.PathPhpRoot)));

                // xdebug
                fileName = Path.GetFileName(definition.Packages.Xdebuguri.AbsoluteUri);
                savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.Xdebuguri, savePath));

                // composer
                savePath = _pathManager.GetPathForTemp("composer.phar");
                Dependencies.Enqueue(new DownloadTask(definition.Packages.ComposerUri, savePath));
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
                    _broker.ReportMessage("Installing PHP...");
                    InternalInstall();
                }
                else
                {
                    _broker.ReportMessage("Uninstalling PHP...");
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
            var root = _pathManager.GetPath(PathKind.PathPhpRoot);

            // install composer
            _broker.ReportMessage("Installing Composer...");
            var composerFile = _pathManager.GetPathForTemp("composer.phar");
            var composerInstallPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"bin\composer");
            File.Move(composerFile, Path.Combine(composerInstallPath, "composer.phar"));

            // create batch execution file
            File.WriteAllText(Path.Combine(composerInstallPath, "composer.bat"), Constants.ComposerBatch);

            // install xdebug
            _broker.ReportMessage("Installing Xdebug...");
            var xdebugFilePath = _pathManager.FindFile(_pathManager.GetPathForTemp(), "php_xdebug*");
            var xdebugFileName = Path.GetFileName(xdebugFilePath) ?? "";
            var xdebugInstallPath = Path.Combine(root, "ext", xdebugFileName);
            File.Move(xdebugFilePath, xdebugInstallPath);

            // save settings (php.ini)
            _broker.ReportMessage("Configuring PHP...");
            using (var config = new TransformingConfigFile(Path.Combine(root, "php.ini"), Constants.PhpConfig))
            {
                config.Transform("{XDEBUG_PATH}", _pathManager.EnsureForwardSlash(xdebugInstallPath));
            }
            
            // add to env path
            _broker.ReportMessage("Adding PHP to environment variable...");
            _pathManager.AddPathEnvironmentVar(root);
            _pathManager.AddPathEnvironmentVar(composerInstallPath);
        }

        private void InternalUninstall()
        {
            // remove from env
            _broker.ReportMessage("Removing PHP from environment variable...");
            var composerInstallPath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"bin\composer");
            _pathManager.RemovePathEnvironmentVar(composerInstallPath);
            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.PathPhpRoot));
        }

        #endregion
    }
}
