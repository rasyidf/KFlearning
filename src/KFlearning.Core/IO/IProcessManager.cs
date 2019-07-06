// 
//  PROJECT  :   KFlearning
//  FILENAME :   IProcessManager.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.IO
{
    public interface IProcessManager
    {
        bool IsRunning(string name);

        void Run(string filename, string args);
        void RunWait(string filename, string args);
        void RunJob(string filename, string args);
        void TerminateJob(string processName);
    }
}