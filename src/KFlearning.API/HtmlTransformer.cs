using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KFlearning.API
{
    public class HtmlTransformer : IHtmlTransformer
    {
        private static readonly Regex Pattern = new Regex(@"<img\s[^>]*?src\s*=\s*[\""](?<url>.*?)[\""][^>]*?>");

        public string TransformHtml(string html)
        {
            return Pattern.Replace(html, Evaluator);
        }

        private string Evaluator(Match match)
        {
            var imageData = DownloadImageAsBase64(match.Groups["url"].Value).Result;
            return "data:image/jpeg;base64," + imageData;
        }

        public async Task<string> DownloadImageAsBase64(string url)
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
