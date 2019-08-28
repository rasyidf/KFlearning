using System.Collections.Generic;
using Newtonsoft.Json;

namespace KFlearning.Core.API
{
    public class PostResponse
    {
        [JsonProperty("posts")]
        public List<Post> Posts { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("page_total")]
        public int Total { get; set; }
    }
}
