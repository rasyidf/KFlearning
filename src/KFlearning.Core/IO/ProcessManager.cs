// 
//  PROJECT  :   KFlearning
//  FILENAME :   ProcessManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;
using System.Diagnostics;
using System.Linq;

namespace KFlearning.Core.IO
{
    public class ProcessManager : IProcessManager
    {
        public bool IsRunning(string name)
        {
            var processes = Process.GetProcessesByName("httpd");
            return processes.Length > 0;
        }

        public void Run(string filename, string args, bool show = false)
        {
            Process.Start(filename, args);
        }

        public void RunWait(string filename, string args, bool show = false)
        {
            Process.Start(filename, args)?.WaitForExit();
        }

        public void RunJob(string filename, string args, bool show = false)
        {
            Process.Start(filename, args);
        }

        public void TerminateJob(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            if (!processes.Any()) return;
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}