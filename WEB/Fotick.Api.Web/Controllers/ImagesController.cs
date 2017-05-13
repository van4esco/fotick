using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fotick.Api.DAL.Repositories;
using Fotick.Api.DAL.Entities;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imagesRepository;

        public ImagesController(IUserRepository userRepository, IImageRepository imagesRepository)
        {
            _userRepository = userRepository;
            _imagesRepository = imagesRepository;
        }


        [HttpPost("{userName}")]
        public async Task<IActionResult> Load([FromRoute]string userName,IEnumerable<string> images)
        {
            try
            {
                var user = await _userRepository.FindByUserName(userName);
                if (user == null)
                    return BadRequest();
                foreach (var item in images)
                {
                    var image = new Image()
                    {
                        Url = item,
                        UserId = user.Id
                    };
                    //Extract to service
                    await _imagesRepository.Add(image);
                }
                return Ok();
            }
            catch(Exception e)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("{userName}/sell")]
        public async Task<IActionResult> Sell(string userName, [FromBody]IEnumerable<string> images)
        {
            var user = await _userRepository.FindByUserName(userName);
            if (user == null)
                return BadRequest();
            foreach (var item in images)
            {
                var image = await _imagesRepository.FindByUrl(item);
                if(image == null)
                {
                    image = new Image()
                    {
                        Url = item,
                        UserId = user.Id,
                        IsForSale = true
                    };
                    await _imagesRepository.Add(image);
                }
                else{
                    image.IsForSale = true;
                    await _imagesRepository.Update(image);
                }
                //TODO add tags generation
                //Extract to service
                return Ok();
            }
            return Ok();
        }

    }
}
