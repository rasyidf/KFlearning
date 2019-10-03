// 
//  PROJECT  :   KFlearning
//  FILENAME :   MathHelper.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace KFlearning.Core
{
    public static class Helpers
    {
        public static int CalculatePercentage(int current, int total)
        {
            return (int) Math.Round((double) current / total * 100, 0);
        }

        public static void ExtractAll(this ZipFile zip, string outputPath, Action<int> progressCallback = null,
            CancellationToken cancellation = default(CancellationToken))
        {
            var count = zip.Count;
            int i = 0;
            foreach (ZipEntry entry in zip)
            {
                cancellation.ThrowIfCancellationRequested();

                i++;
                if (!entry.IsFile) continue;

                string entryFileName = entry.Name;
                byte[] buffer = new byte[4096];

                string fullZipToPath = Path.Combine(outputPath, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName?.Length > 0)
                {
                    Directory.CreateDirectory(directoryName);
                }

                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    Stream zipStream = zip.GetInputStream(entry);
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
                }

                var progress = (int) Math.Round((double) i / count * 100, 0);
                progressCallback?.Invoke(progress);
            }
        }
    }
}