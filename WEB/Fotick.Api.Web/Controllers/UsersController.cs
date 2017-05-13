using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fotick.Api.DAL.Entities;
using Fotick.Api.DAL.Repositories;

namespace Fotick.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string userName)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(userName))
                {
                    return BadRequest();
                }
                var user = await _userRepository.FindByUserName(userName);
                if (user != null)
                    return Ok();
                user = new User
                {
                    Login = userName,
                    UserName = userName
                };
                await _userRepository.Add(user);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var guid = Guid.Empty;
                if (!Guid.TryParse(id, out guid))
                    return BadRequest();
                await _userRepository.Delete(guid);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
