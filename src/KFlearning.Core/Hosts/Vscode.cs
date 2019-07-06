// 
//  PROJECT  :   KFlearning
//  FILENAME :   Vscode.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.IO;
using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
{
    public class Vscode : IVscode
    {
        private readonly IPathManager _pathManager;
        private readonly IProcessManager _processManager;

        public Vscode(IProcessManager processManager, IPathManager pathManager)
        {
            _processManager = processManager;
            _pathManager = pathManager;
        }

        public void OpenFolder(string path)
        {
            _processManager.Run(_pathManager.GetPath(ExecutableFile.Vscode), path);
        }

        public void InstallExtension(string path)
        {
            _processManager.RunWait(_pathManager.GetPath(ExecutableFile.Vscode),
                "--install-extension " + Path.GetFileName(path));
        }
    }
}