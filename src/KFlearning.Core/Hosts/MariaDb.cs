// 
//  PROJECT  :   KFlearning
//  FILENAME :   MariaDb.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using KFlearning.Core.IO;

namespace KFlearning.Core.Hosts
{
    public class MariaDb : IMariaDb
    {
        private readonly IProcessManager _processManager;

        public MariaDb(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void Start()
        {
            _processManager.RunJob(_processManager.GetPath(PathKind.MariadbExe), "--console");
        }

        public void Stop()
        {
            _processManager.TerminateJob(Constants.MariadbProcessName);
        }

        public bool IsRunning()
        {
            return _processManager.IsRunning(Constants.MariadbProcessName);
        }
    }
}