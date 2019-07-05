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
        private readonly IProcessManager _pathManager;

        public Vscode(IProcessManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void OpenFolder(string path)
        {
            _pathManager.Run(_pathManager.GetPath(PathKind.VscodeExe), path);
        }

        public void InstallExtension(string path)
        {
            _pathManager.RunWait(_pathManager.GetPath(PathKind.VscodeExe),
                "--install-extension " + Path.GetFileName(path));
        }
    }
}