using ShopLibrary.DAL;
using ShopLibrary.Models;
using System.Data.Common;

namespace ShopLibrary.Services
{
    public class UsersService
    {
        private readonly UsersDAL _usersDAL;

        public UsersService()
        {

        }
        public UsersService(string provider,string connectionString)
        {
            var factory=DbProviderFactories.GetFactory(provider);
            _usersDAL = new UsersDAL(factory,connectionString);
        }

        public bool IsUserRegistered(string userName)
        {
            var user= GetUserByUserName(userName);
            return user.Name==userName;
        }

        public User GetUserByUserName(string userName)
        {
            return _usersDAL.GetUserByUserName(userName);
        }

        public bool Insert(User newUser)
        {
            return _usersDAL.Insert(newUser);
        }
    }
}
