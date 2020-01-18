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

        public string NetworkCode
        {
            get => Settings.Default.NetworkCode;
            set => Settings.Default.NetworkCode = value;
        }
    }
}
