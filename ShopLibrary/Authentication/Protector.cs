using Microsoft.EntityFrameworkCore;
using ShopLibrary.Authentication.Interfaces;
using ShopLibrary.Entityes;
using ShopLibrary.Interfaces;
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
        public async Task<bool> RegisterAsync(string userName,SecureString password, CancellationToken token=default)
        {
            var foundedUser = await _usersRepository.Items.FirstOrDefaultAsync(x=>x.Name==userName,token).ConfigureAwait(false);
            if (foundedUser!=null)
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
            var insertResult=await _usersRepository.AddAsync(newUser);
            if (insertResult != null)
                return true;
            return false;
        }

        public async Task<bool> LogInAsync(string userName,SecureString password, CancellationToken token=default)
        {
            var user = await _usersRepository.Items.FirstOrDefaultAsync(x => x.Name == userName,token).ConfigureAwait(false);
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
