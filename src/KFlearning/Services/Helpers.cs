using System.Reflection;
using KFlearning.Properties;

namespace KFlearning.Services
{
    public static class Helpers
    {
        public static string GetVersionString()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return $"v{version.Major}.{version.Minor} build \"{Settings.Default.BuildName}\"";
        }
    }
}
