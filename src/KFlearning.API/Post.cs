using KFlearning.API.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KFlearning.API
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Post
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
        
        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }
        
        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("title.rendered")]
        public string Title { get; set; }

        [JsonProperty("content.rendered")]
        public string Content { get; set; }

        [JsonProperty("featured_media")]
        public int FeaturedMedia { get; set; }
        
        [JsonProperty("categories")]
        public List<int> Categories { get; set; }

        [JsonProperty("tags")]
        public List<int> Tags { get; set; }
    }
}
