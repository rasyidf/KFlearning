using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KFlearning.API
{
    public class KodesianaService : IKodesianaService
    {
        private const int TutorialCategoryId = 2;

        private static HttpClient _client = new HttpClient();
        
        public async Task<PostInfo> GetPostsAsync(CancellationToken cancellation, IEnumerable<int> category = null, IEnumerable<int> tags = null, int offset = 0)
        {
            var uri = new Uri($"https://kodesiana.com/wp-json/wp/v2/posts" +
                $"?offset={offset}" +
                (category == null ? "": $"?category={ArrayToUri(category)}") +
                (tags == null ? "" : $"?tag={tags}") +
                $"&_field=id,date,modified,link,title,featured_media,cetegories,tags");
            var result = await _client.GetAsync(uri, cancellation);
            result.EnsureSuccessStatusCode();

            var total = result.Headers.GetValues("X-WP-TotalPages");
            var stream = await result.Content.ReadAsStreamAsync();
            var posts = DeserializeStream<List<Post>>(stream);

            return new PostInfo(Convert.ToInt32(total.First()), offset, posts);
        }


        public async Task<IEnumerable<Taxonomy>> GetCategoriesAsync(CancellationToken cancellation, int offset = 0)
        {
            var uri = new Uri($"https://kodesiana.com/wp-json/wp/v2/categories" +
                $"?offset={offset}" +
                $"&_field=id,date,modified,link,title,featured_media,cetegories,tags");
            var result = await _client.GetAsync(uri, cancellation);
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return DeserializeStream<List<Taxonomy>>(stream);
        }

        public async Task<IEnumerable<Taxonomy>> GetTagsAsync(CancellationToken cancellation, int offset = 0)
        {
            var uri = new Uri($"https://kodesiana.com/wp-json/wp/v2/tags" +
                $"?offset={offset}" +
                $"&_field=id,date,modified,link,title,featured_media,cetegories,tags");
            var result = await _client.GetAsync(uri, cancellation);
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return DeserializeStream<List<Taxonomy>>(stream);
        }

        public async Task<Post> GetPostAsync(CancellationToken cancellation, int postId)
        {
            var uri = new Uri($"https://kodesiana.com/wp-json/wp/v2/post/{postId}" +
                $"&_field=id,date,modified,link,title,featured_media,cetegories,tags,content");
            var result = await _client.GetAsync(uri, cancellation);
            result.EnsureSuccessStatusCode();

            var stream = await result.Content.ReadAsStreamAsync();
            return DeserializeStream<Post>(stream);
        }

        private string ArrayToUri<T>(IEnumerable<T> array)
        {
            return string.Join(",", array);
        }
        
        private static T DeserializeStream<T>(Stream stream)
        {
            if (stream == null || !stream.CanSeek) return default(T);
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters =
                    {
                        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                    }
                };
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
