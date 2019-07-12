namespace KFlearning.Core.Services.Graph
{
    public interface IProgressBroker
    {
        void ReportMessage(string message);
        void ReportProgress(int progressPercentage);
    }
}
