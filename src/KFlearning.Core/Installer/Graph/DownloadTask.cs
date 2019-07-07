using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace KFlearning.Core.Installer.Graph
{
    public class DownloadTask : ITaskNode, IDisposable
    {
        private readonly ManualResetEventSlim _resetEvent;
        private readonly WebClient _client;

        private readonly string _savePath;
        private readonly Uri _uri;
        private Exception _error;

        public string TaskName => "Download file";
        public bool HasDependencies => false;
        public Queue<ITaskNode> Dependencies => null;

        public DownloadTask(Uri uri, string savePath)
        {
            _uri = uri;
            _savePath = savePath;

            _resetEvent = new ManualResetEventSlim(false);
            _client = new WebClient();
            _client.DownloadProgressChanged += Client_DownloadProgressChanged;
            _client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _error = e.Error;
            _resetEvent.Set();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // e.ProgressPercentage
        }

        public bool Run(CancellationToken cancellation)
        {
            try
            {
                _client.DownloadFileAsync(_uri, _savePath);
                _resetEvent.Wait(cancellation);
                if (_error == null) return true;
                // ...
                return false;
            }
            catch (OperationCanceledException)
            {
                _client.CancelAsync();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void Dispose()
        {
            _resetEvent?.Dispose();
            if (_client == null) return;
            _client.DownloadProgressChanged -= Client_DownloadProgressChanged;
            _client.DownloadFileCompleted -= Client_DownloadFileCompleted;
            _client.Dispose();
        }
    }
}
