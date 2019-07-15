// 
//  PROJECT  :   KFlearning
//  FILENAME :   IFileSystemManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Threading;

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