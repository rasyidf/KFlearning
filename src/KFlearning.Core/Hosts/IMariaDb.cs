namespace KFlearning.Core.Hosts
{
    public interface IMariaDb
    {
        bool IsRunning();
        void Start();
        void Stop();
    }
}