// 
//  PROJECT  :   KFlearning
//  FILENAME :   Post.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KFlearning.Core.API
{
    public class Post
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("date"), JsonConverter(typeof(UnixDateTimeConverter))] 
        public DateTime Date { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("series")] public string Series { get; set; }

        [JsonProperty("level")] public int Level { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("source_url")] public string SourceUrl { get; set; }
    }
}