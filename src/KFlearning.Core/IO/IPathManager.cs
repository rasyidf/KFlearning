// 
//  PROJECT  :   KFlearning
//  FILENAME :   IPathManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Threading;

namespace KFlearning.Core.IO
{
    public interface IPathManager
    {
        void InitializePaths();

        string GetPath(PathKind path);
        string GetPathForAlias(string domainName);
        string GetPathForTemp(string filename = "");
        string FindFile(string searchPath, string filename);

        string EnsureForwardSlash(string path);
        string EnsureBackslashEnding(string path);

        void LaunchUri(string uri);
        void LaunchExplorer(string path);

        void RecursiveDelete(string path);
        void RecursiveDelete(string path, CancellationToken token);
        void RecursiveMoveDirectory(string source, string destination);
        void RecursiveMoveDirectory(string source, string destination, CancellationToken token);

        void AddPathEnvironmentVar(string path);
        void RemovePathEnvironmentVar(string path);
    }
}