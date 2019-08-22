// 
//  PROJECT  :   KFlearning
//  FILENAME :   IPathManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

namespace KFlearning.Core.IO
{
    public interface IPathManager
    {
        string Combine(PathKind path, params string[] parts);
        string Combine(params string[] parts);
        string GetPath(PathKind path);
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