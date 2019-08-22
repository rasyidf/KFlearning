// 
//  PROJECT  :   KFlearning
//  FILENAME :   Series.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using LiteDB;

#endregion

namespace KFlearning.Core.DAL
{
    public class Series
    {
        [BsonId] public string Title { get; set; }
    }
}