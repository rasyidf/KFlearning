using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

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
