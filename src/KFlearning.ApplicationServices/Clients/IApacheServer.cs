namespace KFlearning.ApplicationServices.Clients
{
    public interface IApacheServer
    {
        void Start();
        void Stop();
        bool IsRunning();

        string CreateDomainName(string alias);
        void CreateAlias(string alias, string path);
        void RemoveAlias(string alias);
    }
}