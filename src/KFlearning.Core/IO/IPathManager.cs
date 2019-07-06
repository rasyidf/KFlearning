// 
//  PROJECT  :   KFlearning
//  FILENAME :   IPathManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.
namespace KFlearning.Core.IO
{
    public interface IPathManager
    {
        string GetPath(PathKind path);
        string GetPath(ExecutableFile file);
        string GetPath(TemplateFile file);
        string GetPathForAlias(string domainName);
        string EnsureBackslashEnding(string path);
    }
}