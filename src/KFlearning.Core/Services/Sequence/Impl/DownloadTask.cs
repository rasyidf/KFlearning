using System;
using System.IO;
using System.Net;
using System.Threading;

namespace KFlearning.Core.Services.Sequence.Impl
{
    public class DownloadTask : ITaskNode, IDisposable
    {
        #region Fields
        
        private readonly ManualResetEventSlim _resetEvent;
        private readonly WebClient _client;
        private readonly string _savePath;
        private readonly Uri _uri;

        private IProgressBroker _broker;
        private Exception _error;

        #endregion

        #region ITaskNode Properties
        
        public string TaskName => "Downloading: " + _uri;

        public void Run(InstallerDefinition definition, CancellationToken cancellation)
        {
            _broker = definition.ResolveService<IProgressBroker>();

            try
            {
                var dir = Path.GetDirectoryName(_savePath) ?? "";
                Directory.CreateDirectory(dir);

                _client.DownloadFileAsync(_uri, _savePath);
                _resetEvent.Wait(cancellation);

                _broker.ReportNodeProgress(100);
                if (_error != null)
                {
                    _broker.ReportMessage("Download error. " + _error);
                }
                else
                {
                    _broker.ReportMessage("Download completed.");
                }
            }
            catch (OperationCanceledException)
            {
                _client.CancelAsync();
                _broker.ReportMessage("Download canceled.");
                _broker.ReportNodeProgress(100);
            }
            catch (Exception e)
            {
                _broker.ReportMessage(e.ToString());
                _broker.ReportNodeProgress(100);
            }
            finally
            {
                _resetEvent.Set();
            }
        }

        #endregion

        #region Constructor
        
        public DownloadTask(Uri uri, string savePath)
        {
            _uri = uri;
            _savePath = savePath;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            
            _resetEvent = new ManualResetEventSlim(false);
            _client = new WebClient();
            _client.Headers.Add("User-Agent", "Wget/1.13.4 (linux-gnu)");
            _client.DownloadProgressChanged += Client_DownloadProgressChanged;
            _client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        #endregion

        #region Private Methods
        
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _error = e.Error;
            _resetEvent.Set();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _broker.ReportNodeProgress(e.ProgressPercentage);
        }

        #endregion

        #region IDisposable Methods
        
        public void Dispose()
        {
            _resetEvent?.Dispose();
            if (_client == null) return;
            _client.DownloadProgressChanged -= Client_DownloadProgressChanged;
            _client.DownloadFileCompleted -= Client_DownloadFileCompleted;
            _client.Dispose();
        } 

        #endregion
    }
}
