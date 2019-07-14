using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace KFlearning.Core.IO
{
    public class ZipExtractor
    {
        public event EventHandler<ZipExtractEventArgs> StatusChanged;

        public void ExtractAll(string zipPath, string outputPath)
        {
            using (var zip = new ZipFile(zipPath))
            {
                var count = zip.Count;
                int i = 0;
                foreach (ZipEntry entry in zip)
                {
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
                    OnStatusChanged(new ZipExtractEventArgs(progress));
                }   
            }
        }

        protected virtual void OnStatusChanged(ZipExtractEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }
    }
}
