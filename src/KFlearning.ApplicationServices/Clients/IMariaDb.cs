namespace KFlearning.ApplicationServices.Clients
{
    public interface IMariaDb
    {
        bool IsRunning();
        void Start();
        void Stop();
    }
}