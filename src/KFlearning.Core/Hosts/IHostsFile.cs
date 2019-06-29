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