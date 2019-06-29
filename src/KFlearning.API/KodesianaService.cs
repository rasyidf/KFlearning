﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KFlearning.API
{
    public class KodesianaService : IKodesianaService
    {
        private const string EndpointBase = "https://api.kodesiana.com";

        public static HttpClient Client = new HttpClient();

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

        public async Task<IEnumerable<Post>> GetPostsAsync(string series = null)
        {
            var uri = CreateUri(string.IsNullOrEmpty(series) ? "/posts" : "/posts?series=" + series);
            var response = await Client.GetStreamAsync(uri);

            return DeserializeStream<List<Post>>(response);
        }

        public async Task<IEnumerable<Post>> FindPostAsync(string title, string series = null)
        {
            var uri = CreateUri(string.IsNullOrEmpty(series) ? "/find?q=" + title : $"/find?q={title}&series={series}");
            var response = await Client.GetStreamAsync(uri);

            return DeserializeStream<List<Post>>(response);
        }

        public async Task<IEnumerable<string>> GetSeriesAsync()
        {
            var uri = CreateUri("/series");
            var response = await Client.GetStreamAsync(uri);

            return DeserializeStream<List<string>>(response);
        }

        private string CreateUri(string path)
        {
            return EndpointBase + path;
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
