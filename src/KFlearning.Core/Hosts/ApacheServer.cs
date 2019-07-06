// 
//  PROJECT  :   KFlearning
//  FILENAME :   ApacheServer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.IO;
using System.Text;
using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
{
    public class ApacheServer : IApacheServer
    {
        private readonly IPathManager _pathManager;
        private readonly IProcessManager _processManager;

        public ApacheServer(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void Start()
        {
            _processManager.RunJob(_pathManager.GetPath(ExecutableFile.Httpd), "");
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
            var aliasFileName = Path.Combine(_pathManager.GetPath(PathKind.ApacheSitesAliasRoot), domainName + ".conf");

            var sb = new StringBuilder(Constants.VirtualHostTemplate);
            sb.Replace("{ROOT}", _pathManager.EnsureBackslashEnding(path));
            sb.Replace("{DOMAIN}", domainName);
            File.WriteAllText(aliasFileName, sb.ToString());
        }

        public void RemoveAlias(string domainName)
        {
            var aliasFileName = Path.Combine(_pathManager.GetPath(PathKind.ApacheSitesAliasRoot), domainName + ".conf");
            File.Delete(aliasFileName);
        }
    }
}