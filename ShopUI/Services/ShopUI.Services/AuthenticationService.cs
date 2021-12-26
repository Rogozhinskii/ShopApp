using ShopLibrary.Authentication;
using ShopLibrary.Authentication.Interfaces;
using ShopUI.Services.Interfaces;
using System.Security;
using System.Threading.Tasks;

namespace ShopUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {       
        private readonly IProtector _protector;

        public AuthenticationService(IRepositoryManager repositoryManager)
        {
            _protector = new Protector(repositoryManager.UsersService);

        }
        public Task<bool> LogIn(string userName, SecureString password)
        {
            return Task.Run(() => { return 
                _protector.LogIn(userName, password); });
        }

        public Task<bool> Register(string userName, SecureString password)
        {
            return Task.Run(()=> _protector.Register(userName, password));
        }
    }
}
