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
        string GetPath(PathKind path);
        string GetPath(ExecutableFile file);
        string GetPath(TemplateFile file);
        string GetPathForAlias(string domainName);
        string GetPathForTemp(string filename);
        
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