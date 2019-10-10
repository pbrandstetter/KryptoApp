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
    public class MessageController : ControllerBase
    {

        private readonly DataStorage dataStorage;

        public MessageController(DataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        [HttpGet]
        [Route("/api/message/new")]
        public IActionResult newMessages([FromQuery] string username, [FromQuery] string password)
        {
            IEnumerable<User> users = dataStorage.Users.Where(user1 => user1.Username == username).Select(user => user);
            if (users.Count() != 0)
            {
                SHA512 sHA512 = SHA512.Create(password);
                password = Encoding.ASCII.GetString(sHA512.Hash);
                if (password != users.First().Password)
                {
                    return BadRequest("Incorrect password");
                }
            }
            IEnumerable<Message> messages = dataStorage.Messages.Where(message => message.Receiver.Username == username && message.isNew).Select(message => message);
            messages.All(message => message.isNew = false);
            return Ok(messages);
        }

        [HttpGet]
        [Route("/api/message/")]
        public IActionResult getAllMessages([FromQuery] string username, [FromQuery] string password)
        {
            IEnumerable<User> users = dataStorage.Users.Where(user1 => user1.Username == username).Select(user => user);
            if (users.Count() != 0)
            {
                SHA512 sHA512 = SHA512.Create(password);
                password = Encoding.ASCII.GetString(sHA512.Hash);
                if (password != users.First().Password)
                {
                    return BadRequest("Incorrect password");
                }
            }
            IEnumerable<Message> messages = dataStorage.Messages.Where(message => message.Receiver.Username == username).Select(message => message);
            messages.All(message => message.isNew = false);
            return Ok(messages);
        }

        [HttpPost]
        [Route("/api/message/send")]
        public async Task<IActionResult> sendMessage([FromBody] Message message, [FromQuery] string sendUsername, [FromQuery] string password, [FromQuery] string receiveUsername)
        {
            IEnumerable<User> users = dataStorage.Users.Where(user1 => user1.Username == sendUsername).Select(user => user);
            if (users.Count() != 0)
            {
                SHA512 sHA512 = SHA512.Create(password);
                password = Encoding.ASCII.GetString(sHA512.Hash);
                if (password != users.First().Password)
                {
                    return BadRequest("Incorrect password");
                }
            }
            message.isNew = true;
            message.Sender = dataStorage.Users.Where(user => user.Username == sendUsername).First();
            message.Receiver = dataStorage.Users.Where(user => user.Username == sendUsername).First();
            message.Date = DateTime.Now;
            User receiver = dataStorage.Users.Where(user1 => user1.Username == receiveUsername).Select(user => user).First();
            var encrypted = await GetEncryptedData(receiver.PublicKey, message.Text);
            message.Text = Encoding.ASCII.GetString(encrypted.ciphertext);
            message.EncryptedIV = encrypted.encryptedIV;
            message.EncryptedKey = encrypted.encryptedKey;
            return Ok();
        }

        static async Task<(byte[] encryptedKey, byte[] encryptedIV, byte[] ciphertext)> GetEncryptedData(RSAParameters publicKey, string text)
        {
            byte[] ciphertext;
            byte[] key;
            byte[] iv;

            // Use AES to encrypt secret message.
            using (var aes = new AesCng())
            {
                key = aes.Key;
                iv = aes.IV;
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        await swEncrypt.WriteAsync("Secret Message");
                    }

                    ciphertext = msEncrypt.ToArray();
                }
            }

            // Use RSA to encrypt AES key/IV
            using (var rsa = new RSACng())
            {
                // Import given public key to RSA
                rsa.ImportParameters(publicKey);

                // Encrypt symmetric key/IV using RSA (public key).
                // Return encrypted key/IV and ciphertext
                return (encryptedKey: rsa.Encrypt(key, RSAEncryptionPadding.OaepSHA512),
                    encryptedIV: rsa.Encrypt(iv, RSAEncryptionPadding.OaepSHA512),
                    ciphertext);
            }
        }
    }
}