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
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace KFlearning.Core
{
    internal static class Helpers
    {
        private const char CR = '\r';
        private const char LF = '\n';
        private const char NULL = '\0';

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

        public static long CountLines(string path)
        {
            if (path.Contains(".vscode") || path.EndsWith(".json") || path.EndsWith(".kfl")) return 0;

            var lineCount = 0L;
            var byteBuffer = new byte[1024 * 1024];
            var detectedEol = NULL;
            var currentChar = NULL;

            using (var stream = new FileStream(path, FileMode.Open))
            {
                int bytesRead;
                while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        currentChar = (char) byteBuffer[i];
                        if (detectedEol != NULL)
                        {
                            if (currentChar == detectedEol) lineCount++;
                        }
                        else if (currentChar == LF || currentChar == CR)
                        {
                            detectedEol = currentChar;
                            lineCount++;
                        }
                    }
                }

                if (currentChar != LF && currentChar != CR && currentChar != NULL)
                    lineCount++;
            }

            return lineCount;
        }
    }
}