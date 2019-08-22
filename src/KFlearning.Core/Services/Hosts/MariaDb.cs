// 
//  PROJECT  :   KFlearning
//  FILENAME :   MariaDb.cs
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
    public class MariaDb : IMariaDb
    {
        private readonly IPathManager _pathManager;
        private readonly IProcessManager _processManager;

        public bool IsRunning => _processManager.IsRunning(Constants.MariadbProcessName);

        public MariaDb(IProcessManager processManager, IPathManager pathManager)
        {
            _processManager = processManager;
            _pathManager = pathManager;
        }

        public void Start()
        {
            _processManager.RunJob(_pathManager.GetPath(PathKind.ExeMariadb), "--console");
        }

        public void Stop()
        {
            _processManager.TerminateJob(Constants.MariadbProcessName);
        }
    }
}