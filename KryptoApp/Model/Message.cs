using System;

namespace KryptoApp.Model
{
    public class Message
    {
        public DateTime Date { get; set; }

        public User sender { get; set; }

        public User receiver { get; set; }

        public string text { get; set; }
    }
}
