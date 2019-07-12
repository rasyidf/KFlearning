// 
//  PROJECT  :   KFlearning
//  FILENAME :   HostEntry.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

namespace KFlearning.Core.Services
{
    public class HostEntry
    {
        public HostEntry(string ipAddress, string hostname)
        {
            IpAddress = ipAddress;
            Hostname = hostname;
        }

        public string IpAddress { get; }

        public string Hostname { get; }
    }
}