using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class InitializeDirectoriesTask : ITaskNode
    {
        #region Fields
        
        private IPathManager _pathManager;
        private IProgressBroker _broker;
        private InstallMode _mode;

        #endregion

        #region Properties
        
        public string TaskName => "Initialize directories";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        #endregion

        #region Public Methods

        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportProgress(-1);
                var baseDir = _pathManager.GetPath(PathKind.PathBase);
                if (_mode == InstallMode.Install)
                {
                    _broker.ReportMessage("Creating directories...");

                    Directory.CreateDirectory(baseDir);
                    Directory.CreateDirectory(Path.Combine(baseDir, "bin"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"bin\httpd"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"bin\mariadb"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"bin\mingw"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"bin\php"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"bin\vscode"));

                    Directory.CreateDirectory(Path.Combine(baseDir, "etc"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache\alias"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\apache\sites-enabled"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\composer"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\kflearning"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\phpMyAdmin"));
                    Directory.CreateDirectory(Path.Combine(baseDir, @"etc\templates"));

                    Directory.CreateDirectory(Path.Combine(baseDir, "ide"));
                    Directory.CreateDirectory(Path.Combine(baseDir, "repos"));
                }
                else
                {
                    _broker.ReportMessage("Deleting directories...");

                    _pathManager.RecursiveDelete(Path.Combine(baseDir, "bin"), cancellation);
                    _pathManager.RecursiveDelete(Path.Combine(baseDir, "etc"), cancellation);
                    _pathManager.RecursiveDelete(Path.Combine(baseDir, "ide"), cancellation);
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
    }
}
