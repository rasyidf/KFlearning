// 
//  PROJECT  :   KFlearning
//  FILENAME :   IOHelpers.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace KFlearning.Core.IO
{
    public static class IOHelpers
    {
        public static void ExtractAll(this ZipFile zip, string extractPath)
        {
            foreach (ZipEntry entry in zip)
            {
                if (!entry.IsFile) continue;

                string entryFileName = entry.Name;
                byte[] buffer = new byte[4096];

                string fullZipToPath = Path.Combine(extractPath, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName?.Length > 0) Directory.CreateDirectory(directoryName);

                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    Stream zipStream = zip.GetInputStream(entry);
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
                }
            }
        }
    }
}