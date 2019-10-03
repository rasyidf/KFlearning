using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KFlearning.Core.API
{
    public class Post
    {
        [JsonProperty("id")] 
        public int Id { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("title")] 
        public string Title { get; set; }

        [JsonProperty("series")] 
        public string Series { get; set; }

        [JsonProperty("level")] 
        public int Level { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("source_url")] 
        public string SourceUrl { get; set; }
    }
}
