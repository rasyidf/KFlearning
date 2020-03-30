// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : AuthorizationService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Security.Cryptography;
using System.Text;

namespace KFlearning.Core.Security
{
    public interface IAuthorizationService : IDisposable
    {
        string GenerateAuthorization(string username);
        byte[] GetKey(string key);
    }

    public class AuthorizationService : IAuthorizationService
    {
        private readonly MD5 _hasher = new MD5Cng();

        public string GenerateAuthorization(string username)
        {
            var content = _hasher.ComputeHash(Encoding.UTF8.GetBytes(username));
            return BitConverter.ToString(content).Replace("-", "").ToLowerInvariant();
        }

        public byte[] GetKey(string key)
        {
            return _hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        public void Dispose()
        {
            _hasher.Dispose();
        }
    }
}