using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fotick.Api.DAL.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fotick.Api.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {

        private readonly IImageRepository _imagesRepository;

        public HomeController(IImageRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public IActionResult Index(string tag)
        {
            if(!string.IsNullOrWhiteSpace(tag))
            {
                var images = _imagesRepository.F
            }
                return View();
        }
    }
}
