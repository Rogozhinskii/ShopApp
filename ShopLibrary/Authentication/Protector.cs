using ShopLibrary.DAL;
using ShopLibrary.Models;
using ShopLibrary.Services;
using System.Security.Cryptography;
using System.Text;

namespace ShopLibrary.Authentication
{
    public class Protector
    {
        private readonly UsersService _usersService;
        public Protector(UsersService usersService)
        {
            _usersService = usersService;
        }
        public bool Register(string userName,string password)
        {
            var isRegisters = _usersService.IsUserRegistered(userName);
            if (isRegisters)
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
            return _usersService.Insert(newUser);
        }

        public bool LogIn(string userName,string password)
        {
            var user=_usersService.GetUserByUserName(userName);
            if (user.Name != userName)
            {
                return false;
            }
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return saltedhashedPassword == user.SaltedHashedPassword;
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha=SHA256.Create();
            var saltedPasword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPasword)));

        }
    }
}
