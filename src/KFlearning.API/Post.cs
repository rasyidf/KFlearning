using System;
using Newtonsoft.Json;

namespace KFlearning.API
{
    public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("category")]
        public string Series { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("link")]
        public string Url { get; set; }
        
        [JsonProperty("link")]
        public string SourceUrl { get; set; }
    }
}
