using ShopLibrary.DAL;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services.Interfaces;
using System.Data.Common;

namespace ShopLibrary.Services
{
    public class UsersService: IUsersService
    {
        private readonly IRepository<User> _usersRepository;

       
        public UsersService(DbProviderFactory factory,string connectionString)
        {            
            _usersRepository = new UsersRepository(factory,connectionString);
        }

        public bool IsUserRegistered(string userName)
        {
            var user= GetUserByUserName(userName);
            return user.Name==userName;
        }

        public User GetUserByUserName(string userName)
        {
            return _usersRepository.Find(userName);
        }

        public bool AddNewUser(User newUser)
        {
            return _usersRepository.Insert(newUser);
        }
    }
}
