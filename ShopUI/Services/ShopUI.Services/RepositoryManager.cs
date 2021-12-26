using ShopLibrary.DAL.Repositories;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
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

        private readonly IRepository<Customer> _customersRepository;
        public IRepository<Customer> CustomersRepository => _customersRepository;

        public RepositoryManager(IProviderFactoryService providerFactoryService,ConnectionStringSettingsCollection connectionStringCollections)
        {
            var sqlConnection = connectionStringCollections[ConnectionStringNames.sqlConnection];
            _usersService = new UsersService(providerFactoryService.GetFactory(sqlConnection.ProviderName), sqlConnection.ConnectionString);
            _customersRepository = new CustomersRepository(providerFactoryService.GetFactory(sqlConnection.ProviderName), sqlConnection.ConnectionString);

        }
    }
}
