// 
//  PROJECT  :   KFlearning
//  FILENAME :   IDatabaseContext.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using LiteDB;

namespace KFlearning.Core.Entities
{
    public interface IDatabaseContext
    {
        LiteDatabase Database { get; }
        LiteCollection<Article> Articles { get; }
        LiteCollection<Content> Contents { get; }
        LiteCollection<Project> Projects { get; }
        LiteCollection<Series> Series { get; }
    }
}