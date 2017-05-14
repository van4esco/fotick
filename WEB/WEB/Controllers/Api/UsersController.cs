using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL;
using System.Threading.Tasks;
using DAL.Entities;
using System.Web.Http;
using Newtonsoft.Json;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/Users")]
    public class UsersController : ApiController
    {

        [HttpPost]
        public System.Web.Mvc.ActionResult Post([FromBody]string userName)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(userName))
                {
                    return new System.Web.Mvc.HttpStatusCodeResult(400);
                }
                using (var db = FontickDbContext.Create())
                {
                    var user = db.Users.FirstOrDefault(p => p.UserName == userName);
                    if (user != null)
                    {
                        return new System.Web.Mvc.HttpStatusCodeResult(200);
                    }
                    user = new User
                    {
                        Login = userName,
                        UserName = userName
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    return new System.Web.Mvc.HttpStatusCodeResult(200);
                }
            }
            catch (Exception e)
            {
                
                return new System.Web.Mvc.HttpStatusCodeResult(500);
            }
        }

    }
}
