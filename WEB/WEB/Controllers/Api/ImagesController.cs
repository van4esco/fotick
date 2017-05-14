using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL;
using System.Threading.Tasks;
using DAL.Entities;
using System.Web.Http;
using Newtonsoft.Json;
using WEB.App_Start;
using System.Net.Http;
using System.Web.Hosting;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ApiController
    {

        [Route("{userName}")]
        [HttpPost()]
        public System.Web.Mvc.ActionResult Load([FromUri]string userName,[FromBody]IEnumerable<string> images)
        {
            try
            {
                var db = FontickDbContext.Create();
                var user = db.Users.FirstOrDefault(p => p.UserName == userName);
                if (user == null)
                    return new System.Web.Mvc.HttpStatusCodeResult(500);
                foreach (var item in images)
                {
                    if (db.Images.FirstOrDefault(p=>p.Url == item) == null)
                    {
                        var image = new Image()
                        {
                            Url = item,
                            UserId = user.Id
                        };
                        db.Images.Add(image);
                    }
                }
                db.SaveChanges();
                return new System.Web.Mvc.HttpStatusCodeResult(200);
            }
            catch(Exception e)
            {
                return new System.Web.Mvc.HttpStatusCodeResult(500);
            }
        }

        [Route("{userName}/sell")]
        [HttpPost]
        public async Task<System.Web.Mvc.ActionResult> SellAsync(string userName, [FromBody]IEnumerable<string> images)
        {
            var db = FontickDbContext.Create();
            var tagsManager = new TagsManager();
            var user = db.Users.FirstOrDefault(p=>p.UserName == userName);
            if (user == null)
                return new System.Web.Mvc.HttpStatusCodeResult(400);
            foreach (var item in images)
            {
                var image = db.Images.FirstOrDefault(p => p.Url == item);
                if (image == null)
                {
                    image = new Image()
                    {
                        Url = item,
                        UserId = user.Id,
                        IsForSale = true
                    };
                    db.Images.Add(image);
                }
                else
                {
                    image.IsForSale = true;
                    db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                }
                var tags = await tagsManager.GetTags(item);
                if (image.Tags == null)
                    image.Tags = new List<Tag>();
                foreach (var i in tags)
                {
                    image.Tags.Add(i);
                }
                db.SaveChanges();
            }
            return new System.Web.Mvc.HttpStatusCodeResult(200);
        }

        [Route("Tags")]
        [HttpGet]
        public string GetTags([FromUri]string url){
            var db = FontickDbContext.Create();
            return JsonConvert.SerializeObject(db.Images.FirstOrDefault(p => p.Url == url)?.Tags);
        }
        [Route("User")]
        [HttpGet()]
        public string GetUser([FromUri]string url)
        {
            var db = FontickDbContext.Create();
            return db.Users.FirstOrDefault(p => p.Images!=null &&p.Images.Any(x => x.Url == url)).Login;
        }

        [Route("{userName}/files")]
        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<IHttpActionResult> UploadSingleFile([FromUri]string userName)
        {
            try
            {
                var path = HostingEnvironment.MapPath($"~/Files/Images/{userName}");
                var streamProvider = new MultipartFormDataStreamProvider(path);
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                return Ok();
            }
            catch(Exception){
                return BadRequest();
            }
        }
    }
}
