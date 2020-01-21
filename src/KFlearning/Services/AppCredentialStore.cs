// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : AppCredentialStore.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using KFlearning.Core.Services;
using KFlearning.Properties;

namespace KFlearning.Services
{
    public class AppCredentialStore : ICredentialStorage
    {
        public string AccessCode
        {
            get => Settings.Default.AccessCode;
            set => Settings.Default.AccessCode = value;
        }
    }
}