// 
//  PROJECT  :   KFlearning
//  FILENAME :   Vscode.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.IO;

namespace KFlearning.Core.Services
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
            _processManager.Run(_pathManager.GetPath(PathKind.ExeVscode), path);
        }
    }
}