using System.IO;
using System.Text;
using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
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
            _processManager.TerminateJob(Constants.HttpdProcessName);
        }

        public bool IsRunning()
        {
            return _processManager.IsRunning(Constants.HttpdProcessName);
        }

        public void CreateAlias(string domainName, string path)
        {
            var aliasFileName = Path.Combine(_processManager.GetPath(PathKind.ApacheSitesRoot), domainName + ".conf");

            var sb = new StringBuilder(Constants.VirtualHostTemplate);
            sb.Replace("{ROOT}", _processManager.EnsureBackslashEnding(path));
            sb.Replace("{DOMAIN}", domainName);
            File.WriteAllText(aliasFileName, sb.ToString());
        }

        public void RemoveAlias(string domainName)
        {
            var aliasFileName = Path.Combine(_processManager.GetPath(PathKind.ApacheSitesRoot), domainName + ".conf");
            File.Delete(aliasFileName);
        }
    }
}
