// 
//  PROJECT  :   KFlearning
//  FILENAME :   HostEntry.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

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