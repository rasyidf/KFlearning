using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using KFlearning.Core;
using KFlearning.Core.Hosts;
using KFlearning.Core.IO;

namespace KFlearning.IDE.ApplicationServices
{
    public class WebServer : IWebServer
    {
        private readonly IPathManager _pathManager;
        private readonly IHostsFile _hosts;
        private readonly IApacheHttpd _apache;
        private readonly IMariaDb _mariaDb;

        public event EventHandler<StatusChangedEventArgs> StatusUpdate;
        public event EventHandler RunningStatusChanged;

        public bool IsRunning { get; private set; }

        public WebServer(IPathManager pathManager, IApacheHttpd apache, IMariaDb mariaDb, IHostsFile hosts)
        {
            _pathManager = pathManager;
            _apache = apache;
            _mariaDb = mariaDb;
            _hosts = hosts;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                _apache.Start();
                _mariaDb.Start();

                await Task.Delay(5000);
                if (!_apache.IsRunning || !_mariaDb.IsRunning)
                {
                    _apache.Stop();
                    _mariaDb.Stop();
                    IsRunning = false;
                }
                else
                {
                    IsRunning = true;
                }
            });
        }

        public void Stop()
        {
            _apache.Stop();
            _mariaDb.Stop();
        }

        public void CreateAlias(string domainName, string path)
        {
            var sb = new StringBuilder(Constants.VirtualHostTemplate);
            sb.Replace("{ROOT}", _pathManager.EnsureBackslashEnding(path));
            sb.Replace("{DOMAIN}", domainName);

            var aliasFileName = _pathManager.GetPathForAlias(domainName);
            File.WriteAllText(aliasFileName, sb.ToString());

            _hosts.AddEntry(domainName);
        }

        public void RemoveAlias(string domainName)
        {
            File.Delete(_pathManager.GetPathForAlias(domainName));
            _hosts.RemoveEntry(domainName);
        }
    }
}
