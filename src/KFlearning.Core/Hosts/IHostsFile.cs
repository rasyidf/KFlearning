// 
//  PROJECT  :   KFlearning
//  FILENAME :   IHostsFile.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  NPM      :   065118116
// 
//  This file is part of KFlearning, licensed under MIT license.

using System.Collections.Generic;

namespace KFlearning.Core.Hosts
{
    public interface IHostsFile
    {
        void AddEntry(string domain);
        void RemoveEntry(string domain);
        IEnumerable<HostEntry> EnumerateDomains();
    }
}