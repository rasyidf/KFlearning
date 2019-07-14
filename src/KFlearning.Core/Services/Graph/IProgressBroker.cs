namespace KFlearning.Core.Services.Graph
{
    public interface IProgressBroker
    {
        void ReportMessage(string message);
        void ReportNodeProgress(int progressPercentage);
        void ReportSequenceProgress(int progressPercentage);
    }
}
