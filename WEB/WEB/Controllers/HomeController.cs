using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Web.Mvc;
using DAL;
using System.Net;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fotick.Api.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string tag)
        {
            using (var db = FontickDbContext.Create())
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    return View(db.Images.Where(p => p.IsForSale && p.Tags != null && p.Tags.Any(pp => pp.Text.Intersect(tag).Count() > 0)).Select(p => p.Url));
                }
                return View();
            }
        }

        public FileStreamResult DownloadItem(string url)
        {
            Stream stream = null;
            stream = GetImageStreamFromUrl(url);
            return File(stream, "image/jpeg", "ImageName");
        }

        public Stream GetImageStreamFromUrl(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpWebReponse.GetResponseStream())
                {
                    return stream;
                }
            }
        }
    }
}
