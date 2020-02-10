// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : CredentialService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using KFlearning.Core.IO;

namespace KFlearning.Core.Security
{
    public interface ICredentialService : IDisposable
    {
        bool Verify(string accessCode);
        void Save(string accessCode);
    }

    public class CredentialService : ICredentialService
    {
        private const string CredentialSettingsKey = "Credential.Settings";

        private readonly SHA256Managed _hasher = new SHA256Managed();
        private readonly IPersistanceStorage _storage;

        public CredentialService(IPersistanceStorage storage)
        {
            _storage = storage;
        }

        public bool Verify(string accessCode)
        {
            var currentCode = _hasher.ComputeHash(Encoding.UTF8.GetBytes(accessCode));
            var stored = _storage.Retrieve<CredentialSettings>(CredentialSettingsKey) ?? CreateDefaultSettings();
            var storedCode = Convert.FromBase64String(stored.AccessCode);

            return storedCode.SequenceEqual(currentCode);
        }

        public void Save(string accessCode)
        {
            var buffer = _hasher.ComputeHash(Encoding.UTF8.GetBytes(accessCode));
            var store = new CredentialSettings
            {
                AccessCode = Convert.ToBase64String(buffer)
            };
            _storage.Store(CredentialSettingsKey, store);
        }

        private CredentialSettings CreateDefaultSettings()
        {
            return new CredentialSettings {AccessCode = "8D0rHYDtvdMCvYjf6XdzNoQE7eg/mA8KpKGSYQCTpxY="};
        }

        public void Dispose()
        {
            _hasher.Dispose();
        }
    }
}