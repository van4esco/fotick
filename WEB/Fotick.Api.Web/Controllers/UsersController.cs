using System;

using Microsoft.AspNetCore.Mvc;
using Fotick.Api.DAL.Entities;
using Fotick.Api.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        public  IActionResult Post([FromBody]string userName)
        {
            try
            {
                _logger.LogDebug($"UserName: {userName}");
                if(string.IsNullOrWhiteSpace(userName))
                {
                    return BadRequest();
                }
                var user =  _userRepository.FindByUserName(userName);
                if (user != null)
                {
                    _logger.LogDebug($"User allready exist: {userName}");
                    return Ok();
                }
                user = new User
                {
                    Login = userName,
                    UserName = userName
                };
                 _userRepository.Add(user);
                _logger.LogDebug($"Created user: {userName}");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public  IActionResult Delete(string id)
        {
            try
            {
                var guid = Guid.Empty;
                if (!Guid.TryParse(id, out guid))
                    return BadRequest();
                 _userRepository.Delete(guid);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
