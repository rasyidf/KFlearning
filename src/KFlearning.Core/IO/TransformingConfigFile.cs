using System;
using System.IO;
using System.Text;

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
