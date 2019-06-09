using System.Collections.Generic;

namespace KFlearning.ApplicationServices.Clients
{
    public interface IHostsFile
    {
        void AddEntry(string domain);
        void RemoveEntry(string domain);
        IEnumerable<HostEntry> EnumerateDomains();
    }
}