﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KFlearning.Core.API
{
    public interface IHtmlTransformer
    {
        void TransformHtmlForSave(string filePath);
        void TransformHtmlForStyle(string filePath);
    }

    public class HtmlTransformer : IHtmlTransformer
    {
        private static readonly Regex Pattern = new Regex(@"<img\s[^>]*?src\s*=\s*[\""](?<url>.*?)[\""][^>]*?>");

        public void TransformHtmlForSave(string filePath)
        {
            var input = File.ReadAllText(filePath);
            var content = Pattern.Replace(input, Evaluator);
            File.WriteAllText(filePath, content);
        }

        public void TransformHtmlForStyle(string filePath)
        {
            var input = File.ReadAllText(filePath);
            var content = new StringBuilder();
            content.AppendLine(CoreResources.HtmlBodyStart);
            content.AppendLine(input);
            content.Replace("[csharp]", "<pre>");
            content.Replace("[/csharp]", "</pre>");
            content.AppendLine(CoreResources.HtmlBodyEnd);
            
            File.WriteAllText(filePath, content.ToString());
        }

        private string Evaluator(Match match)
        {
            try
            {
                var imageData = DownloadImageAsBase64(match.Groups["url"].Value).Result;
                return "data:image/jpeg;base64," + imageData;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private async Task<string> DownloadImageAsBase64(string url)
        {
            var decodedUrl = WebUtility.HtmlDecode(url);
            using (var request = await KodesianaService.Client.GetStreamAsync(decodedUrl))
            using (var image = Image.FromStream(request))
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
