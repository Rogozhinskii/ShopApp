using ShopLibrary.DAL.Repositories;
using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using ShopUI.Core;
using ShopUI.Services.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ShopUI.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        public IUsersService _usersService;
        public IUsersService UsersService => _usersService;

        private readonly IRepository<Customer> _customersRepository;
        public IRepository<Customer> CustomersRepository => _customersRepository;

        public List<object> Repositories { get; private set; } = new();

        public RepositoryManager(IProviderFactoryService providerFactoryService,ConnectionStringSettingsCollection connectionStringCollections)
        {
            var sqlConnection = connectionStringCollections[ConnectionStringNames.sqlConnection];
            var accessConnection=connectionStringCollections[ConnectionStringNames.oleConnection];
            _usersService = new UsersService(providerFactoryService.GetFactory(sqlConnection.ProviderName), sqlConnection.ConnectionString);
            _customersRepository = new CustomersRepository(providerFactoryService.GetFactory(sqlConnection.ProviderName), sqlConnection.ConnectionString);            
            Repositories.Add(_customersRepository);
        }

        private void RegisterRepositories(params Repository<object>[] repository)
        {
            Repositories.AddRange(repository);
        }
    }
}
