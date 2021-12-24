using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using ShopUI.Core;
using ShopUI.Services.Interfaces;

namespace ShopUI.Services
{
    internal class RepositoryManager : IRepositoryManager
    {
        public IUsersService _usersService;
        public IUsersService UsersService => _usersService;

        public RepositoryManager(IProviderFactoryService providerFactoryService)
        {
            _usersService = new UsersService(providerFactoryService.SqlFactory,ConnectionStringNames.sqlConnection);
        }
    }
}
