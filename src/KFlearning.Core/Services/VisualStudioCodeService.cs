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

using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{    
    public interface IVisualStudioCodeService
    {
        void OpenFolder(string path);
    }

    public class VisualStudioCodeService : IVisualStudioCodeService
    {
        private readonly IProcessManager _processManager;
        private readonly IPathManager _path;
        
        public VisualStudioCodeService(IProcessManager processManager, IPathManager path)
        {
            _processManager = processManager;
            _path = path;
        }

        public void OpenFolder(string path)
        {
            _path.DiscoverVisualStudioCode(out string vscode);
            _processManager.Run(vscode, $"\"{path}\"");
        }
    }
}