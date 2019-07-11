﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using KFlearning.Core.Hosts;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class VscodeTask : ITaskNode
    {
        #region Fields
        
        private IProgressBroker _broker;
        private IPathManager _pathManager;
        private IVscode _vscode;
        private InstallMode _mode; 

        #endregion

        #region Properties

        public string TaskName => "Visual Studio Code";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; private set; } 

        #endregion

        #region Public Methods

        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _vscode = definition.ResolveService<IVscode>();
            _mode = definition.Mode;

            Dependencies = new Queue<ITaskNode>();
            if (definition.Mode == InstallMode.Install)
            {
                var fileName = Path.GetFileName(definition.Packages.VscodeUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);

                Dependencies.Enqueue(new DownloadTask(definition.Packages.VscodeUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.VscodeInstallRoot)));
                foreach (Uri extUri in definition.Packages.VscodeExtensions)
                {
                    fileName = Path.GetFileName(extUri.AbsoluteUri);
                    savePath = _pathManager.GetPathForTemp(fileName);

                    Dependencies.Enqueue(new DownloadTask(extUri, savePath));
                }

                foreach (Uri extUri in definition.Packages.ProjectTemplates)
                {
                    fileName = Path.GetFileName(extUri.AbsoluteUri);
                    savePath = Path.Combine(_pathManager.GetPath(TemplateFile.RootDir), fileName);

                    Dependencies.Enqueue(new DownloadTask(extUri, savePath));
                }
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(_pathManager.GetPath(PathKind.VscodeInstallRoot)));
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
            var vscodeRoot = _pathManager.GetPath(PathKind.VscodeInstallRoot);
            var extRoot = _pathManager.GetPathForTemp("");

            // create data directory
            _broker.ReportMessage("Creating data directory...");
            Directory.CreateDirectory(Path.Combine(vscodeRoot, "data"));
            
            // install extensions
            _broker.ReportMessage("Installing extensions...");
            List<string> ses = Directory.EnumerateFiles(extRoot, "*.vsix").ToList();
            for (var i = 0; i < ses.Count; i++)
            {
                var percentage = (int) Math.Round((double) (i + 1) / ses.Count * 100, 0);

                _broker.ReportProgress(percentage);
                _vscode.InstallExtension(ses[i]);
            }

            // save settings
            var vscodeSettingsFile = Path.Combine(vscodeRoot, @"data\user-data\settings.json");
            var settings = new StringBuilder(Constants.VscodeConfig);
            settings.Replace("{PHP_PATH}", _pathManager.GetPath(PathKind.PhpInstallRoot));
            File.WriteAllText(vscodeSettingsFile, settings.ToString());

            // add to environment variable
            _broker.ReportProgress(80);
            _broker.ReportMessage("Adding Visual Studio Code to environment variable...");

            var path = Path.Combine(vscodeRoot, "bin");
            _pathManager.AddPathEnvironmentVar(path);
        }

        private void InternalUninstall()
        {
            // remove from environment variable
            _broker.ReportProgress(70);
            _broker.ReportMessage("Removing Visual Studio Code from environment variable...");
            
            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.VscodeInstallRoot));
        }
        
        #endregion
    }
}
