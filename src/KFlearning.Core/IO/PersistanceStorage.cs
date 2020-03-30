// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : PersistanceStorage.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.IO;
using System.Security.Cryptography;
using KFlearning.Core.Security;
using Newtonsoft.Json;

namespace KFlearning.Core.IO
{
    public interface IPersistanceStorage
    {
        void Store(string name, object value);
        T Retrieve<T>(string name);
    }

    public class PersistanceStorage : IPersistanceStorage
    {
        private readonly Aes _aes;
        private readonly IPathManager _path;

        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        public PersistanceStorage(IPathManager path, IAuthorizationService authorization)
        {
            _path = path;
            _aes = new AesManaged
            {
                Key = authorization.GetKey(Environment.MachineName),
                IV = authorization.GetKey(Environment.MachineName)
            };

            var dirPath = _path.GetPath(PathKind.PersistanceDirectory);
            Directory.CreateDirectory(dirPath);
        }

        public void Store(string name, object value)
        {
            var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + ".kfl");

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var cryptoStream = new CryptoStream(fileStream, _aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                _serializer.Serialize(writer, value);
            }
        }

        public T Retrieve<T>(string name)
        {
            try
            {
                var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + ".kfl");
                if (!File.Exists(path)) return default;

                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var cryptoStream = new CryptoStream(fileStream, _aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<T>(jsonReader);
                }
            }
            catch
            {
                return default;
            }
        }
    }
}