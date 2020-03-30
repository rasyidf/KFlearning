// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : ApiServiceBase.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KFlearning.Core.API.Model;
using KFlearning.Core.Security;
using Newtonsoft.Json;

namespace KFlearning.Core.API
{
    public abstract class ApiServiceBase
    {
#if DEBUG
        private const string BaseUri = "http://kflearning-board.test/api";
#else
        private const string BaseUri = "http://kflearning.kodesiana.com/api";
#endif
        
        private static readonly HttpClient Client = new HttpClient();
        private readonly IAuthorizationService _authorizationService;

        protected ApiServiceBase(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        protected async Task<ApiResponseBody> GetResponse(ApiRequestBody body)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, BaseUri))
            {
                message.Headers.Add("KF-Authorization",
                    _authorizationService.GenerateAuthorization(body.User.Username));
                message.Content = SerializeContent(body);

                var result = await Client.SendAsync(message);
                var res = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponseBody>(res);
            }
        }

        private StringContent SerializeContent(ApiRequestBody body)
        {
            var serialized = JsonConvert.SerializeObject(body);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            return content;
        }
    }
}