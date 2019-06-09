using System.Diagnostics;

namespace KFlearning.ApplicationServices
{
    public class ApplicationHelpers : IApplicationHelpers
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }
    }
}
