// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : Helpers.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using Microsoft.Win32;

namespace KFlearning.Core
{
    internal static class Helpers
    {
        public static string TrimLongText(this string path, int maxLength = 40)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return path.Length <= maxLength ? path : path.Substring(0, maxLength) + "...";
        }

        public static int GetIntValue(this RegistryKey key, string name, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : Convert.ToInt32(key.GetValue(name, defaultValue));
        }

        public static string GetStringValue(this RegistryKey key, string name, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : key.GetValue(name, defaultValue).ToString();
        }
    }
}