namespace KFlearning.ApplicationServices.Clients
{
    public class HostEntry
    {
        public string IpAddress { get; }

        public string Hostname { get; }

        public HostEntry(string ipAddress, string hostname)
        {
            IpAddress = ipAddress;
            Hostname = hostname;
        }
    }
}
