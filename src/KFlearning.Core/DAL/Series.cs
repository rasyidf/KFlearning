// 
//  PROJECT  :   KFlearning
//  FILENAME :   Series.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using LiteDB;

namespace KFlearning.Core.DAL
{
    public class Series
    {
        [BsonId] public string Title { get; set; }
    }
}