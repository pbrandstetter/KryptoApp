using KryptoApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KryptoServer.Services
{
    public class DataStorage
    {
        public DataStorage()
        {
            this.users = new IEnumerable.Empty();
        }

        readonly IEnumerable<User> users { get; set; }
    }
}
