// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProcessManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Diagnostics;

namespace KFlearning.Core.IO
{
    public class ProcessManager : IProcessManager
    {
        public bool IsRunning(string name)
        {
            var processes = Process.GetProcessesByName("httpd");
            return processes.Length > 0;
        }

        public void Run(string filename, string args)
        {
            Process.Start(filename, args);
        }

        public void RunWait(string filename, string args)
        {
            throw new NotImplementedException();
        }

        public void RunJob(string filename, string args)
        {
            throw new NotImplementedException();
        }

        public void TerminateJob(string processName)
        {
            throw new NotImplementedException();
        }
    }
}