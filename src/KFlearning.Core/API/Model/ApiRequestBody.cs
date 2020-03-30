// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : ApiRequestBody.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using Newtonsoft.Json;

namespace KFlearning.Core.API.Model
{
    public class ApiRequestBody
    {
        [JsonProperty("action")] public string Action { get; set; }

        [JsonProperty("user")] public UserProfile User { get; set; }
    }
}