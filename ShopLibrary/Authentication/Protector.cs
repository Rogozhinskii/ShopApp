using ShopLibrary.Authentication.Interfaces;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ShopLibrary.Authentication
{
    public class Protector: IProtector
    {        
        private readonly IRepository<User> _usersRepository;

        public Protector(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public bool Register(string userName,SecureString password)
        {
            var foundedUser = _usersRepository.Find(userName);
            if(foundedUser.Name== userName)
                throw new Exception("Пользователь с таким именем зарегистрирован");
            var rnd=RandomNumberGenerator.Create();
            var saltBytes=new byte[16];
            rnd.GetBytes(saltBytes);
            var saltText=Convert.ToBase64String(saltBytes);
            var saltedhashedPassword = SaltAndHashPassword(password, saltText);
            var newUser = new User
            {
                Name = userName,
                Salt = saltText,
                SaltedHashedPassword = saltedhashedPassword,
            };
            return _usersRepository.Insert(newUser);
        }

        public bool LogIn(string userName,SecureString password)
        {
            var user= _usersRepository.Find(userName);
            if (user.Name != userName)
            {
                return false;
            }
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return saltedhashedPassword == user.SaltedHashedPassword;
        }

        private static string SaltAndHashPassword(SecureString password, string salt)
        {
            var sha=SHA256.Create();
            var saltedPasword = password.GetPasswordAsString() + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPasword)));

        }
    }
}
