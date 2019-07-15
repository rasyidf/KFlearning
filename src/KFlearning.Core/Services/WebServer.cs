using System;
using System.IO;
using System.Threading.Tasks;
using KFlearning.Core.IO;

namespace KFlearning.Core.Services
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

        public string GenerateDomainName(string title)
        {
            return $"{_pathManager.StripInvalidFileName(title).ToLowerInvariant()}.{Constants.DomainName}";
        }

        public void CreateAlias(string domainName, string path)
        {
            var docPath = _pathManager.EnsureBackslashEnding(_pathManager.EnsureForwardSlash(path));

            // create alias
            var aliasFileName = _pathManager.GetPathForAlias(domainName);
            using (var alias = new TransformingConfigFile(aliasFileName, Constants.AliasTemplate))
            {
                alias.Transform("{ALIAS_PATH}", docPath);
                alias.Transform("{ALIAS_NAME}", CreateAliasName(domainName));
            }

            // create virtual host
            var virtualHostFileName = _pathManager.GetPathForVirtualHost(domainName);
            using (var virtualHost = new TransformingConfigFile(virtualHostFileName, Constants.VirtualHostTemplate))
            {
                virtualHost.Transform("{REPO_PATH}", docPath);
                virtualHost.Transform("{REPO_DOMAIN}", domainName);
            }
            
            // add to hosts
            _hosts.AddEntry(domainName);
        }

        public void RemoveAlias(string domainName)
        {
            _hosts.RemoveEntry(domainName);
            File.Delete(_pathManager.GetPathForAlias(domainName));
            File.Delete(_pathManager.GetPathForVirtualHost(domainName));
        }

        private string CreateAliasName(string domainName)
        {
            return domainName.Substring(0, domainName.Length - 4);
        }
    }
}
