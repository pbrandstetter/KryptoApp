using KrypoLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KryptoApp.Services
{
    public class DataStorage
    {
        public DataStorage()
        {
            this.Users = Enumerable.Empty<User>();
            this.Messages = Enumerable.Empty<Message>();
        }

        public IEnumerable<User> Users { get; }
        public IEnumerable<Message> Messages { get; }
    }
}
