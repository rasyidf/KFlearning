// 
//  PROJECT  :   KFlearning
//  FILENAME :   Article.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using LiteDB;

#endregion

namespace KFlearning.Core.IDE.Data
{
    public class Article
    {
        [BsonId] public int ArticleId { get; set; }

        public string Series { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public string Url { get; set; }

        public string SourceUrl { get; set; }
    }
}