using System;
using System.ComponentModel.DataAnnotations;

namespace KrypoLibrary.Model
{
    public class Message
    {
        public DateTime Date { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        [Required]
        public string Text { get; set; }

        public byte[] EncryptedKey { get; set; }

        public byte[] EncryptedIV { get; set; }
        
        public bool isNew { get; set; }
    }
}
