namespace KFlearning.Core.IO
{
    public interface IProcessManager
    {
        string GetPath(PathKind path);
        string EnsureBackslashEnding(string path);

        bool IsRunning(string name);

        void RunWait(string filename, string args);
        void RunJob(string filename, string args);
        void TerminateJob(string processName);
    }
}