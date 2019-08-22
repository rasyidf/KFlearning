// 
//  PROJECT  :   KFlearning
//  FILENAME :   IWebServer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using KFlearning.Core.Services.Installer;

#endregion

namespace KFlearning.Core.Services
{
    public interface IWebServer
    {
        event EventHandler<StatusChangedEventArgs> StatusUpdate;
        event EventHandler RunningStatusChanged;

        bool IsRunning { get; }

        void Start();
        void Stop();
        string GenerateDomainName(string title);
        void CreateAlias(string domainName, string path);
        void RemoveAlias(string domainName);
    }
}