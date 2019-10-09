using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KryptoApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KryptoServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("/api/user/login")]
        public IActionResult login()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/user/logout")]
        public IActionResult logout()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/api/user/register")]
        public IActionResult register([FromBody] User user)
        {
            return Ok(user);
        }

        [HttpGet]
        [Route("/api/user/")]
        public IActionResult getAllUsers()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/user/{id}")]
        public IActionResult getUser([FromRoute] int id)
        {
            return Ok();
        }
    }
}