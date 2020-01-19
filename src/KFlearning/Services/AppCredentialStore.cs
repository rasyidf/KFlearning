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
