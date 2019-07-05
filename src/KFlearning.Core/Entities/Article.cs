// 
//  PROJECT  :   KFlearning
//  FILENAME :   Article.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using LiteDB;

namespace KFlearning.Core.Entities
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