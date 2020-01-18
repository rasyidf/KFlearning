using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace KFlearning.Core.Services
{
    public interface ITemplateService
    {
        IEnumerable<Template> GetTemplates();
        void Extract(Template template, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        private static readonly List<Template> Templates = new List<Template>
        {
            new Template("Konsol (C++)", "cpp_console"),
            new Template("GUI Freeglut (C++)", "cpp_freeglut"),
            new Template("Web (PHP/HTML/CSS/JS)", "web_php"),
            new Template("Python/Jupyter Notebook", "python_anaconda_base"),
            new Template("Kosong", null)
        };

        public IEnumerable<Template> GetTemplates()
        {
            return Templates;
        }

        public void Extract(Template template, string outputPath)
        {
            Extract(template.ResourceName, outputPath);
        }

        private void Extract(string name, string outputPath)
        {
            if (name == null) return;

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
