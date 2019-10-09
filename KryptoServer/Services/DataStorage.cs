using KryptoApp.Model;
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
        }

        private IEnumerable<User> Users { get; }
    }
}
