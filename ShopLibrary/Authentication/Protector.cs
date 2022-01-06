using ShopLibrary.Authentication.Interfaces;
using ShopLibrary.DAL.Extensions;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
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
        public async Task<bool> Register(string userName,SecureString password)
        {
            var foundedUser = (await _usersRepository.Select(x => x.Name == userName));
            if (foundedUser.Any())
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
            var insertResult=await _usersRepository.Insert(newUser);
            if (insertResult != 0)
                return true;
            return false;
        }

        public async Task<bool> LogIn(string userName,SecureString password)
        {
            var user = (await _usersRepository.Select(x => x.Name == userName)).First();
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return saltedhashedPassword == user.SaltedHashedPassword;
        }

        /// <summary>
        /// Возвращается хешированный пароль с "солью"
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string SaltAndHashPassword(SecureString password, string salt)
        {
            var sha=SHA256.Create();
            var saltedPasword = password.GetPasswordAsString() + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPasword)));

        }
    }
}
