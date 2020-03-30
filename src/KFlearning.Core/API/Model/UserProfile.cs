// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : UserProfile.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using Newtonsoft.Json;

namespace KFlearning.Core.API.Model
{
    public class UserProfile
    {
        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("score1")] public double Score1 { get; set; }

        [JsonProperty("score2")] public double Score2 { get; set; }

        [JsonProperty("score3")] public double Score3 { get; set; }
    }
}