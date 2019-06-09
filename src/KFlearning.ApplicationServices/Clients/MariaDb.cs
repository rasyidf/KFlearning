namespace KFlearning.ApplicationServices.Clients
{
    public class MariaDb : IMariaDb
    {
        private readonly IProcessManager _processManager;

        public MariaDb(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void Start()
        {
            _processManager.RunJob(_processManager.GetPath(PathKind.MariadbExe), "--console");
        }

        public void Stop()
        {
            _processManager.TerminateJob(Strings.MariadbProcessName);
        }

        public bool IsRunning()
        {
            return _processManager.IsRunning(Strings.MariadbProcessName);
        }
    }
}
