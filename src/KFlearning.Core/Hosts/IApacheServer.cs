namespace KFlearning.Core.Hosts
{
    public interface IApacheServer
    {
        void Start();
        void Stop();
        bool IsRunning();

        void CreateAlias(string domainName, string path);
        void RemoveAlias(string domainName);
    }
}