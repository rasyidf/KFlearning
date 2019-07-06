// 
//  PROJECT  :   KFlearning
//  FILENAME :   IMariaDb.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.Hosts
{
    public interface IMariaDb
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}