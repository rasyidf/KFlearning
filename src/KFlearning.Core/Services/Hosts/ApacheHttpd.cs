// 
//  PROJECT  :   KFlearning
//  FILENAME :   ApacheHttpd.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using KFlearning.Core.IO;

#endregion

namespace KFlearning.Core.Services
{
    public class ApacheHttpd : IApacheHttpd
    {
        private readonly IPathManager _pathManager;
        private readonly IProcessManager _processManager;

        public bool IsRunning => _processManager.IsRunning(Constants.HttpdProcessName);

        public ApacheHttpd(IProcessManager processManager, IPathManager pathManager)
        {
            _processManager = processManager;
            _pathManager = pathManager;
        }

        public void Start()
        {
            _processManager.RunJob(_pathManager.GetPath(PathKind.ExeHttpd), "");
        }

        public void Stop()
        {
            _processManager.TerminateJob(Constants.HttpdProcessName);
        }
    }
}