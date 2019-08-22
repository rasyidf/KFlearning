// 
//  PROJECT  :   KFlearning
//  FILENAME :   IMariaDb.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

namespace KFlearning.Core.Services
{
    public interface IMariaDb
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}