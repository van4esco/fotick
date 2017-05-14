using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Fotick.Api.DAL.Repositories;
using Fotick.Api.DAL.Entities;
using Microsoft.Extensions.Logging;
using Fotick.Api.BLL.Managers;
using Fotick.Api.BLL.Contracts;
using System.Threading.Tasks;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imagesRepository;
        private readonly ILogger<ImagesController> _logger;
        private readonly ITagsManager _tagsManager;

        public ImagesController(IUserRepository userRepository, IImageRepository imagesRepository, ILogger<ImagesController> logger, ITagsManager tagsManager)
        {
            _userRepository = userRepository;
            _imagesRepository = imagesRepository;
            _logger = logger;
            _tagsManager = tagsManager;
        }

        [HttpPost("{userName}")]
        public IActionResult Load([FromRoute]string userName,[FromBody]string[] images)
        {
            try
            {
                var user = _userRepository.FindByUserName(userName);
                if (user == null)
                    return BadRequest();
                foreach (var item in images)
                {
                    if (_imagesRepository.FindByUrl(item) == null)
                    {
                        var image = new Image()
                        {
                            Url = item,
                            UserId = user.Id
                        };
                        _imagesRepository.Add(image);
                    }
                    //Extract to service
                }
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("{userName}/sell")]
        public async Task<IActionResult> SellAsync(string userName, [FromBody]IEnumerable<string> images)
        {
            var user = _userRepository.FindByUserName(userName);
            if (user == null)
                return BadRequest();
            foreach (var item in images)
            {
                var image = _imagesRepository.FindByUrl(item);
                if (image == null)
                {
                    image = new Image()
                    {
                        Url = item,
                        UserId = user.Id,
                        IsForSale = true
                    };
                    _imagesRepository.Add(image);
                }
                else
                {
                    image.IsForSale = true;
                    _imagesRepository.Update(image);
                }
                var tags = await _tagsManager.GetTags(item);
                _imagesRepository.AddTags(image.Id, tags);
            }
            return Ok();
        }

        [HttpGet("Tags")]
        public IActionResult GetTags([FromQuery]string url){
            var image = _imagesRepository.FindByUrl(url);
            return Json(_imagesRepository.GetImageTags(image.Id));
        }

    }
}
