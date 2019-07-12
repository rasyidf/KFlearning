﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services.Graph
{
    public class VscodeTask : ITaskNode
    {
        #region Fields
        
        private IProcessManager _processManager;
        private IPathManager _pathManager;
        private IProgressBroker _broker;
        private InstallMode _mode; 

        #endregion

        #region Properties

        public string TaskName => "Visual Studio Code";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; } = new Queue<ITaskNode>();

        #endregion

        #region Public Methods

        public void Configure(InstallerDefinition definition)
        {
            _processManager = definition.ResolveService<IProcessManager>();
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;

            if (definition.Mode == InstallMode.Install)
            {
                var fileName = Path.GetFileName(definition.Packages.VscodeUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);

                Dependencies.Enqueue(new DownloadTask(definition.Packages.VscodeUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.PathVscodeRoot)));
                foreach (Uri extUri in definition.Packages.VscodeExtensions)
                {
                    fileName = Path.GetFileName(extUri.AbsoluteUri);
                    savePath = _pathManager.GetPathForTemp(fileName);

                    Dependencies.Enqueue(new DownloadTask(extUri, savePath));
                }

                foreach (Uri extUri in definition.Packages.ProjectTemplates)
                {
                    fileName = Path.GetFileName(extUri.AbsoluteUri);
                    savePath = Path.Combine(_pathManager.GetPath(PathKind.PathBase), @"etc\templates", fileName);

                    Dependencies.Enqueue(new DownloadTask(extUri, savePath));
                }
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(_pathManager.GetPath(PathKind.PathVscodeRoot)));
            }
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportProgress(0);
                if (_mode == InstallMode.Install)
                {
                    _broker.ReportMessage("Installing Visual Studio Code...");
                    InternalInstall();
                }
                else
                {
                    _broker.ReportMessage("Uninstalling Visual Studio Code...");
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
            // invalidate path caches
            _pathManager.InitializePaths();
            var vscodeRoot = _pathManager.GetPath(PathKind.PathVscodeRoot);
            
            // create data directory
            _broker.ReportMessage("Creating data directory...");
            Directory.CreateDirectory(Path.Combine(vscodeRoot, "data"));
            Directory.CreateDirectory(Path.Combine(vscodeRoot, @"data\user-data"));
            
            // install extensions
            _broker.ReportMessage("Installing extensions...");
            var extRoot = _pathManager.GetPathForTemp();
            List<string> extFiles = Directory.EnumerateFiles(extRoot, "*.vsix").ToList();
            for (var i = 0; i < extFiles.Count; i++)
            {
                var percentage = (int) Math.Round((double) (i + 1) / extFiles.Count * 100, 0);
                _broker.ReportProgress(percentage);
                InstallExtension(extFiles[i]);
            }

            // save settings
            _broker.ReportMessage("Saving global settings...");
            var vscodeSettingsFile = Path.Combine(vscodeRoot, @"data\user-data\settings.json");
            using (var config = new TransformingConfigFile(vscodeSettingsFile, Constants.VscodeConfig))
            {
                var phpExePath = Path.Combine(_pathManager.GetPath(PathKind.PathPhpRoot), "php.exe");
                config.Transform("{PHP_PATH}", _pathManager.EnsureForwardSlash(phpExePath));
            }

            // add to environment variable
            _broker.ReportProgress(80);
            _broker.ReportMessage("Adding Visual Studio Code to environment variable...");
            _pathManager.AddPathEnvironmentVar(Path.Combine(vscodeRoot, "bin"));
        }

        private void InternalUninstall()
        {
            // remove from environment variable
            _broker.ReportProgress(70);
            _broker.ReportMessage("Removing Visual Studio Code from environment variable...");
            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.PathVscodeRoot));
        }

        #endregion

        #region Private Methods
        
        private void InstallExtension(string path)
        {
            _processManager.RunWait(_pathManager.GetPath(PathKind.ExeVscode),
                $"--install-extension \"{Path.GetFileName(path)}\"");
        } 

        #endregion
    }
}