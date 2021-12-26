using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using ShopUI.Core;
using ShopUI.Services.Interfaces;
using System.Configuration;

namespace ShopUI.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        public IUsersService _usersService;
        public IUsersService UsersService => _usersService;

        public RepositoryManager(IProviderFactoryService providerFactoryService,ConnectionStringSettings connectionSettings)
        {
            _usersService = new UsersService(providerFactoryService.GetFactory(connectionSettings.ProviderName), connectionSettings.ConnectionString);
        }
    }
}
