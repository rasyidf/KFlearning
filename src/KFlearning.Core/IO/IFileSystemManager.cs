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

        void DeleteDirectory(string path, CancellationToken token);
        void CopyDirectory(string source, string destination, CancellationToken token);

        void WriteFile(string path, string content);
        void DeleteFile(string source);

        void CreateDirectoryLink(string link, string target);
        void RemoveDirectoryLink(string link);
        bool DirectoryLinkExists(string dir);
        void CreateShortcutOnDesktop(string linkName, string description, string path);
    }
}