// 
//  PROJECT  :   KFlearning
//  FILENAME :   IFileSystemManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Threading;

#endregion

namespace KFlearning.Core.IO
{
    public interface IFileSystemManager
    {
        string FindFile(string searchPath, string filename);
        string FindDirectory(string searchPath, string directoryName);

        void CreateDirectory(string path);
        void DeleteDirectory(string path, CancellationToken token);
        void MoveDirectory(string source, string destination, CancellationToken token);
        void CopyDirectory(string source, string destination, CancellationToken token);

        void MoveFile(string source, string destination);
        void CopyFile(string source, string destination);
        void WriteFile(string path, string content);

        void CreateShortcutOnDesktop(string linkName, string description, string path);
    }
}