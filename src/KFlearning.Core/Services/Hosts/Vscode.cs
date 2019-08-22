// 
//  PROJECT  :   KFlearning
//  FILENAME :   Vscode.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using KFlearning.Core.IO;

#endregion

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