using System;
using System.IO;
using System.Text;

namespace KFlearning.Core.Services
{
    public class Transformable
    {
        public string RelativePath { get; }
        public Func<string> Template { get; }
        public Func<StringBuilder, StringBuilder> TransformFunc { get; }

        public Transformable(string relativePath, Func<string> template, Func<StringBuilder, StringBuilder> transformFunc = null)
        {
            Template = template;
            TransformFunc = transformFunc;
            RelativePath = relativePath;
        }

        public void Transform(string outputPath)
        {
            var path = Path.Combine(outputPath, RelativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            var content = TransformFunc == null
                ? Template.Invoke()
                : TransformFunc.Invoke(new StringBuilder(Template.Invoke())).ToString();
            File.WriteAllText(path, content);
        }
    }
}
