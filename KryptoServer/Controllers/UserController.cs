using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KryptoApp.Model;
using KryptoServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KryptoServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataStorage dataStorage;

        public UserController(DataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        [HttpPost]
        [Route("/api/user/login")]
        public IActionResult login([FromBody] User user)
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
            dataStorage.getUsers().Add(user);
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