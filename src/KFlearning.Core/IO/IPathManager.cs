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
        string Combine(PathKind path, params string[] parts);
        string Combine(params string[] parts);
        string GetPath(PathKind path);
        string GetPathForVirtualHost(string domainName);
        string GetPathForAlias(string domainName);
        string GetPathForTemp(string filename = "");
        string GetFileName(string path);
        string StripInvalidFileName(string path);
        string EnsureForwardSlash(string path);
        string EnsureBackslashEnding(string path);

        void LaunchUri(string uri);
        void LaunchExplorer(string path);
        
        void AddPathEnvironmentVar(string path);
        void RemovePathEnvironmentVar(string path);
    }
}