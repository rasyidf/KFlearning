using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace KFlearning.Core.Diagnostics
{
    internal static class RegistryHelper
    {
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
