﻿// 
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