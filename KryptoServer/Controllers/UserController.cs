using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KrypoLibrary.Model;
using KryptoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KryptoApp.Controllers
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

            IEnumerable<User> users = dataStorage.Users.Where(user1 => user1.Username == user.Username);
            if (users.Count() != 0)
            {
                SHA512 sHA512 = SHA512.Create(user.Password);
                user.Password = Encoding.ASCII.GetString(sHA512.Hash);
                if (user.Password == users.First().Password)
                {
                    return Ok(users.First().Password);
                }
                return BadRequest("Incorrect password");
            }
            return NotFound("User not found.");
        }

        [HttpPost]
        [Route("/api/user/register")]
        public IActionResult register([FromBody] User user)
        {
            if (dataStorage.Users.Where(user1 => user1.Username == user.Username).Count() != 0)
            {
                return BadRequest("Username not available.");
            }
            SHA512 sHA512 = SHA512.Create(user.Password);
            user.Password = Encoding.ASCII.GetString(sHA512.Hash);
            var (publicKey, publicPrivateKey) = GenerateKeyPair();
            user.PublicKey = publicKey;
            user.PrivateKey = publicPrivateKey;
            ((List<User>)dataStorage.Users).Add(user);
            return Ok(user);
        }

        [HttpGet]
        [Route("/api/user")]
        public IActionResult getAllUsers()
        {
            return Ok(dataStorage.Users.Select(s => s.Username));
        }

        [HttpGet]
        [Route("/api/user/{username}")]
        public IActionResult getUser([FromRoute] string username)
        {
            return Ok(dataStorage.Users.Where(user => user.Username == username).Select(user => user.Username));
        }

        static (RSAParameters publicKey, RSAParameters publicPrivateKey) GenerateKeyPair()
        {
            // Create object implementing RSA. Note that this version of the
            // constructor generates a random 2048-bit key pair.
            using (var rsa = new RSACng())
            {
                return (publicKey: rsa.ExportParameters(false),
                    publicPrivateKey: rsa.ExportParameters(true));
            }
        }
    }
}