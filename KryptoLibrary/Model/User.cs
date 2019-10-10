using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace KrypoLibrary.Model
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public RSAParameters PublicKey { get; set; }

        public RSAParameters PrivateKey { get; set; }
    }
}