// 
//  PROJECT  :   KFlearning
//  FILENAME :   TransformingConfigFile.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Text;

#endregion

namespace KFlearning.Core.IO
{
    public class TransformingConfigFile : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly StringBuilder _template;

        public TransformingConfigFile(string path, string template)
        {
            _template = new StringBuilder(template);
            _writer = new StreamWriter(path, false);
        }

        public void Transform(string token, string value)
        {
            if (!_disposedValue) _template.Replace(token, value);
        }

        public void Commit()
        {
            _writer.Write(_template.ToString());
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            Commit();

            if (disposing)
            {
                _writer?.Dispose();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}