using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KryptoApp.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
    }
}