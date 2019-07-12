﻿// 
//  PROJECT  :   KFlearning
//  FILENAME :   IWebServer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System;

namespace KFlearning.Core.Services
{
    public interface IWebServer
    {
        event EventHandler<StatusChangedEventArgs> StatusUpdate;
        event EventHandler RunningStatusChanged;

        bool IsRunning { get; }

        void Start();
        void Stop();
        string CreateDomainName(string title);
        void CreateAlias(string domainName, string path);
        void RemoveAlias(string domainName);
    }
}