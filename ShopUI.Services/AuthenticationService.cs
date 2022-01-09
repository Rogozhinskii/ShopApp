using ShopLibrary.Authentication;
using ShopLibrary.Authentication.Interfaces;
using ShopUI.Services.Interfaces;
using System.Security;

namespace ShopUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IProtector _protector;

        public AuthenticationService(IRepositoryManager repositoryManager)
        {
            //_protector = new Protector(repositoryManager.GetRepository(RepositoryType.Users) as IRepository<User>);

        }
        public Task<bool> LogIn(string userName, SecureString password)
        {
            return Task.Run(() => {
                return _protector.LogIn(userName, password);
            });
        }

        public Task<bool> Register(string userName, SecureString password)
        {
            return Task.Run(() => _protector.Register(userName, password));
        }
    }
}
