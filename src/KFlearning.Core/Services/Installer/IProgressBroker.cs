namespace KFlearning.Core.Services.Installer
{
    public interface IProgressBroker
    {
        void ReportMessage(string message);
        void ReportNodeProgress(int progressPercentage);
        void ReportSequenceProgress(int progressPercentage);
    }
}
