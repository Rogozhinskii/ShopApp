using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Services.Interfaces
{
    public interface IUsersService
    {
        public bool IsUserRegistered(string userName);

        public User GetUserByUserName(string userName);

        public bool AddNewUser(User user);

    }
}
