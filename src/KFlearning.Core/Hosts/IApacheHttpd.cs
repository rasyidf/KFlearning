// 
//  PROJECT  :   KFlearning
//  FILENAME :   IApacheServer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.Hosts
{
    public interface IApacheHttpd
    {
        void Start();
        void Stop();
        bool IsRunning();
    }
}