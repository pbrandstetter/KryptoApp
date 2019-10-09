
namespace KryptoApp.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
    }
}