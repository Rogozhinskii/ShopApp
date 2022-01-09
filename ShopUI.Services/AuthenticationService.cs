using ShopLibrary.Authentication;
using ShopLibrary.Authentication.Interfaces;
using ShopLibrary.Entityes;
using ShopLibrary.Interfaces;
using ShopUI.Services.Interfaces;
using System.Security;

namespace ShopUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IProtector _protector;

        public AuthenticationService(IRepository<User> usersRepository)
        {
            _protector = new Protector(usersRepository);
        }
        public async Task<bool> LogInAsync(string userName, SecureString password,CancellationToken token = default) =>
            await _protector.LogInAsync(userName, password,token);
            
        

        public async Task<bool> RegisterAsync(string userName, SecureString password,CancellationToken token = default) =>
            await _protector.RegisterAsync(userName,password,token);
            
        
    }
}
