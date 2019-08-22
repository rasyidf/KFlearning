// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProcessManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

namespace KFlearning.Core.IO
{
    public interface IProcessManager
    {
        bool IsRunning(string name);

        void Run(string filename, string args, bool show = false);
        void RunWait(string filename, string args, bool show = false);
        void RunJob(string filename, string args, bool show = false);
        void TerminateJob(string processName);
    }
}