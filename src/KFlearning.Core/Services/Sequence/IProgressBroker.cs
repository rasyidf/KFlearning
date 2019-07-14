namespace KFlearning.Core.Services.Sequence
{
    public interface IProgressBroker
    {
        void ReportMessage(string message);
        void ReportNodeProgress(int progressPercentage);
        void ReportSequenceProgress(int progressPercentage);
    }
}
