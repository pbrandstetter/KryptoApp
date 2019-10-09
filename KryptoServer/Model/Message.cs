using System;

namespace KryptoApp.Model
{
    public class Message
    {
        public DateTime Date { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public string Text { get; set; }
    }
}
