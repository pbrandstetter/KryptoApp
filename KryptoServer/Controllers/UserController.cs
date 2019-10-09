using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KryptoApp.Model;
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

            return Ok();
        }

        [HttpPost]
        [Route("/api/user/register")]
        public IActionResult register([FromBody] User user)
        {
            user.Id = dataStorage.Users.Count();
            SHA512 sHA512 = SHA512.Create(user.Password);
            user.Password = Encoding.ASCII.GetString(sHA512.Hash);
            var (publicKey, publicPrivateKey) = GenerateKeyPair();
            user.PublicKey = publicKey;
            user.PrivateKey = publicPrivateKey;
            ((List<User>)dataStorage.Users).Add(user);
            return Ok(user);
        }

        [HttpGet]
        [Route("/api/user/")]
        public IActionResult getAllUsers()
        {
            return Ok(dataStorage.Users.Select(s => (id: s.Id, username: s.Username)));
        }

        [HttpGet]
        [Route("/api/user/{username}")]
        public IActionResult getUser([FromRoute] string username)
        {
            return Ok(dataStorage.Users.Where(user => user.Id == id).Select(user => (id: user.Id, username: user.Username)));
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