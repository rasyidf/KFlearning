using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using KFlearning.Core.IO;

namespace KFlearning.Core.Graph
{
    public class MingwTask : ITaskNode
    {
        #region Fields
        
        private IProgressBroker _broker;
        private IProcessManager _processManager;
        private IPathManager _pathManager;
        private InstallMode _mode;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "MinGW Compiler Suite";
        public bool HasDependencies => true;
        public Queue<ITaskNode> Dependencies { get; } = new Queue<ITaskNode>();

        #endregion

        #region ITaskNode Methods
        
        public void Configure(InstallerDefinition definition)
        {
            _pathManager = definition.ResolveService<IPathManager>();
            _processManager = definition.ResolveService<IProcessManager>();
            _broker = definition.ResolveService<IProgressBroker>();
            _mode = definition.Mode;

            if (definition.Mode == InstallMode.Install)
            {
                var fileName = Path.GetFileName(definition.Packages.MingwUri.AbsoluteUri);
                var savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.MingwUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPath(PathKind.PathMingwRoot)));

                fileName = Path.GetFileName(definition.Packages.GlutUri.AbsoluteUri);
                savePath = _pathManager.GetPathForTemp(fileName);
                Dependencies.Enqueue(new DownloadTask(definition.Packages.GlutUri, savePath));
                Dependencies.Enqueue(new ExtractTask(savePath, _pathManager.GetPathForTemp()));
            }
            else
            {
                Dependencies.Enqueue(new DeleteFilesTask(_pathManager.GetPath(PathKind.PathMingwRoot)));
            }
        } 

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _broker.ReportProgress(-1);
                if (_mode == InstallMode.Install)
                {
                    _broker.ReportMessage("Installing MinGW...");
                    InternalInstall();
                }
                else
                {
                    _broker.ReportMessage("Uninstalling MinGW...");
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
            var root = _pathManager.GetPath(PathKind.PathPhpRoot);

            // install using mingw-get
            var file = Path.Combine(root, @"bin\mingw-get.exe");
            _processManager.RunWait(file, "install gcc g++ gdb mingw32-make");

            // install glut library to mingw
            var glutPath = Path.Combine(_pathManager.GetPathForTemp(), "GLUTMingw32");
            _pathManager.RecursiveMoveDirectory(Path.Combine(glutPath, "lib"), Path.Combine(root, "lib"));
            _pathManager.RecursiveMoveDirectory(Path.Combine(glutPath, "include"), Path.Combine(root, "include"));
            
            // install glut to system
            var glutDllSource = Path.Combine(Path.Combine(glutPath, "glut32.dll"));
            var glutDllDest = Path.Combine(Environment.SystemDirectory, "glut32.dll");
            File.Move(glutDllSource, glutDllDest);

            // add to environment variable
            _broker.ReportProgress(80);
            _broker.ReportMessage("Adding MinGW Compiler Suite to environment variable...");
            _pathManager.AddPathEnvironmentVar(Path.Combine(root, "bin"));
        }

        private void InternalUninstall()
        {
            // remove from environment variable
            _broker.ReportProgress(70);
            _broker.ReportMessage("Removing MinGW Compiler Suite from environment variable...");
            _pathManager.RemovePathEnvironmentVar(_pathManager.GetPath(PathKind.PathMingwRoot));
        }
        
        #endregion
    }
}
