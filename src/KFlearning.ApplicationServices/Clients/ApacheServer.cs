using System.IO;
using System.Text;

namespace KFlearning.ApplicationServices.Clients
{
    public class ApacheServer : IApacheServer
    {
        private readonly IProcessManager _processManager;

        public ApacheServer(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void Start()
        {
            _processManager.RunJob(_processManager.GetPath(PathKind.HttpdExe), "");
        }

        public void Stop()
        {
            _processManager.TerminateJob(Strings.HttpdProcessName);
        }

        public bool IsRunning()
        {
            return _processManager.IsRunning(Strings.HttpdProcessName);
        }

        public void CreateAlias(string alias, string path)
        {
            var domainName = CreateDomainName(alias);
            var aliasFileName = Path.Combine(_processManager.GetPath(PathKind.ApacheSitesRoot), domainName + ".conf");

            var sb = new StringBuilder(Strings.VirtualHostTemplate);
            sb.Replace("{ROOT}", _processManager.EnsureBackslashEnding(path));
            sb.Replace("{DOMAIN}", domainName);
            File.WriteAllText(aliasFileName, sb.ToString());
        }

        public void RemoveAlias(string alias)
        {
            var domainName = CreateDomainName(alias);
            var aliasFileName = Path.Combine(_processManager.GetPath(PathKind.ApacheSitesRoot), domainName + ".conf");
            File.Delete(aliasFileName);
        }

        public string CreateDomainName(string alias)
        {
            return $"{alias}.{Strings.DomainName}";
        }
    }
}
