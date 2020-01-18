using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace KFlearning.Core.Services
{
    public interface ITemplateService
    {
        void Extract(string name, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        public void Extract(string name, string outputPath)
        {
            var content = (byte[]) TemplateResources.ResourceManager.GetObject(name) ??
                          throw new InvalidOperationException();
            using (var ms = new MemoryStream(content))
            using (var zip = new ZipFile(ms))
            {
                foreach (ZipEntry entry in zip)
                {
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
                }
            }
        }
    }
}
