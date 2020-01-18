using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KFlearning.Core.Services
{
    public interface ICredentialService : IDisposable
    {
        bool Verify(string accessCode);
        void SaveAccessCode(string code);
        void SaveNetworkCode(string code);
    }

    public class CredentialService : ICredentialService
    {
        private readonly SHA256Managed _hasher = new SHA256Managed();
        private readonly ICredentialStorage _storage;

        public CredentialService(ICredentialStorage storage)
        {
            _storage = storage;
        }

        public bool Verify(string accessCode)
        {
            var currentCode = _hasher.ComputeHash(Encoding.UTF8.GetBytes(accessCode));
            var storedCode = Convert.FromBase64String(_storage.AccessCode);

            return storedCode.SequenceEqual(currentCode);
        }

        public void SaveAccessCode(string code)
        {
            var buffer = _hasher.ComputeHash(Encoding.UTF8.GetBytes(code));
            _storage.AccessCode = Convert.ToBase64String(buffer);
        }

        public void SaveNetworkCode(string code)
        {
            var buffer = _hasher.ComputeHash(Encoding.UTF8.GetBytes(code));
            _storage.AccessCode = Convert.ToBase64String(buffer);
        }

        public void Dispose()
        {
            _hasher?.Dispose();
        }
    }
}
