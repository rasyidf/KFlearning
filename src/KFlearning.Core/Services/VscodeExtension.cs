using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public class VscodeExtension
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
