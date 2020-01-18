using System;
using Microsoft.Win32;

namespace KFlearning.Core
{
    internal static class Helpers
    {
        public static string TrimLongText(string path, int maxLength = 20)
        {
            return path.Length <= 20 ? path : path.Substring(0, maxLength) + "...";
        }

        public static int GetIntValue(this RegistryKey key, string name, int defaultValue = 0)
        {
            return key == null ? defaultValue : Convert.ToInt32(key.GetValue(name, defaultValue));
        }

        public static string GetStringValue(this RegistryKey key, string name, string defaultValue = "")
        {
            return key == null ? defaultValue : key.GetValue(name, defaultValue).ToString();
        }
    }
}
