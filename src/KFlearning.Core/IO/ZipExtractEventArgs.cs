namespace KFlearning.Core.IO
{
    public class ZipExtractEventArgs
    {
        public int ProgressPercentage { get; }

        public ZipExtractEventArgs(int progressPercentage)
        {
            ProgressPercentage = progressPercentage;
        }
    }
}
