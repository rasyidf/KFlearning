// 
//  PROJECT  :   KFlearning
//  FILENAME :   Content.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using LiteDB;

namespace KFlearning.Core.Entities
{
    public class Content
    {
        [BsonId] public int ContentId { get; set; }

        public int ArticleId { get; set; }

        public string HtmlBody { get; set; }
    }
}