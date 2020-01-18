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

using System;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using KFlearning.Core.Diagnostics;
using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{    
    public interface IVscode
    {
        void OpenFolder(string path);
    }

    public class Vscode : IVscode
    {
        private readonly IProcessManager _processManager;
        private readonly IPathManager _path;
        
        public Vscode(IProcessManager processManager, IPathManager path)
        {
            _processManager = processManager;
            _path = path;
        }

        public void InstallExtension(string path)
        {
            var filePath = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"vscode\bin\code.cmd");
            _processManager.RunWait(filePath, $"--install-extension \"{path}\"");
        }

        public void OpenFolder(string path)
        {
            var filePath = Path.Combine(_path.GetPath(PathKind.InstallRoot), @"vscode\code.exe");
            _processManager.Run(filePath, $"\"{path}\"");
        }
    }
}