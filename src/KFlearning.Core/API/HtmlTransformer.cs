// 
//  PROJECT  :   KFlearning
//  FILENAME :   HtmlTransformer.cs
//  AUTHOR   :   Fahmi Noor Fiqri
//  WEBSITE  : https://kodesiana.com
//  REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
//  This file is part of KFlearning, licensed under MIT license.
//  See this code in repository URL above!

#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#endregion

namespace KFlearning.Core.API
{
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
            var content = Constants.HtmlBodyStart + input + Constants.HtmlBodyEnd;
            File.WriteAllText(filePath, content);
        }

        private string Evaluator(Match match)
        {
            var imageData = DownloadImageAsBase64(match.Groups["url"].Value).Result;
            return "data:image/jpeg;base64," + imageData;
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