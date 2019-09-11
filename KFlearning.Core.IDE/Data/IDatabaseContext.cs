// 
//  PROJECT  :   KFlearning
//  FILENAME :   IDatabaseContext.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

#endregion

using LiteDB;

namespace KFlearning.Core.IDE.Data
{
    public interface IDatabaseContext
    {
        LiteDatabase Database { get; }
        LiteStorage Storage { get; }
        LiteCollection<Article> Articles { get; }
        LiteCollection<Project> Projects { get; }
        LiteCollection<Series> Series { get; }
    }
}