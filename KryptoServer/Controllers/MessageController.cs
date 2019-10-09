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
    public class MessageController : ControllerBase
    {

        [HttpGet]
        [Route("/api/message/new")]
        public IActionResult newMessages([FromQuery] int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("/api/message/")]
        public IActionResult getAllMessages([FromQuery] int userId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("/api/message/send")]
        public IActionResult sendMessage([FromBody] Message message, [FromQuery] int sendId, [FromQuery] int receiveId)
        {
            return Ok();
        }
    }
}