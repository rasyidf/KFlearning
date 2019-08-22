// 
//  PROJECT  :   KFlearning
//  FILENAME :   IHostsFile.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.Collections.Generic;

#endregion

namespace KFlearning.Core.Services
{
    public interface IHostsFile
    {
        void AddEntry(string domain);
        void RemoveEntry(string domain);
        IEnumerable<HostEntry> EnumerateDomains();
    }
}