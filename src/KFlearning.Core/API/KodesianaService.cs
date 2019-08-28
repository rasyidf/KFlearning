// 
//  PROJECT  :   KFlearning
//  FILENAME :   KodesianaService.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace KFlearning.Core.API
{
    public class KodesianaService : IKodesianaService
    {
        public const string EndpointBase = "https://api.kodesiana.com/kflearning";

        public static HttpClient Client = new HttpClient();

        #region Public Methods

        public async Task<bool> IsOnline()
        {
            try
            {
                var result = await Client.GetAsync(EndpointBase);
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetPostAsync(int postId)
        {
            var uri = CreateUri($"/posts/{postId}");
            var response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public Task<PostResponse> GetPostsAsync(int offset, int count)
        {
            return GetPostsAsync(offset, count, string.Empty);
        }

        public async Task<PostResponse> GetPostsAsync(int offset, int count, string series)
        {
            var uri = CreateUri($"/posts?series={series}&offset={offset}&count={count}");
            var response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await DeserializeStream<PostResponse>(response.Content);
        }

        public Task<PostResponse> FindPostAsync(int offset, int count, string title)
        {
            return FindPostAsync(offset, count, title, string.Empty);
        }

        public async Task<PostResponse> FindPostAsync(int offset, int count, string title, string series)
        {
            var uri = CreateUri($"/posts/find?title={title}&series={series}&offset={offset}&count={count}");
            var response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await DeserializeStream<PostResponse>(response.Content);
        }

        public async Task<IEnumerable<string>> GetSeriesAsync()
        {
            var uri = CreateUri("/posts/series");
            var response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await DeserializeStream<List<string>>(response.Content);
        } 

        #endregion

        #region Private Methods

        private string CreateUri(string path)
        {
            return EndpointBase + path;
        }

        private static async Task<T> DeserializeStream<T>(HttpContent content)
        {
            if (content == null) return default;
            using (var stream = await content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters =
                    {
                        new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
                    }
                };

                return serializer.Deserialize<T>(jsonReader);
            }
        } 

        #endregion
    }
}