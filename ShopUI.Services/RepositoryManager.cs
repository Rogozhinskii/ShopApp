using ShopLibrary.Interfaces;
using ShopLibrary.Services;
using ShopUI.Core;
using ShopUI.Services.Interfaces;
using System.Configuration;

namespace ShopUI.Services
{
    public class RepositoryManager// : IRepositoryManager
    {             
        //private readonly IRepository<Customer> _customersRepository;
        //private readonly IRepository<Product> _productsRepository;
        //private readonly IRepository<User> _usersRepository;
        //public List<object> Repositories { get; private set; } = new();

        //public RepositoryManager(ConnectionStringSettingsCollection connectionStringCollections)
        //{
        //    var sqlConnection = connectionStringCollections[ConnectionStringNames.sqlConnection];
        //    var accessConnection = connectionStringCollections[ConnectionStringNames.accessConnection];
        //    var sqlFactory= ProviderFactoryService.GetFactory(DatabaseType.SqlServer);
        //    _usersRepository = new UsersRepository(sqlFactory, sqlConnection.ConnectionString);
        //    _customersRepository = new CustomersRepository(sqlFactory, sqlConnection.ConnectionString);
        //    _productsRepository = new ProductsRepository(ProviderFactoryService.GetFactory(DatabaseType.MsAccess), accessConnection.ConnectionString);
        //    Repositories.Add(_customersRepository);
        //    Repositories.Add(_productsRepository);
        //}

        //public object GetRepository(RepositoryType repositoryType)
        //{
        //    return repositoryType switch
        //    {
        //        RepositoryType.Customers => _customersRepository,
        //        RepositoryType.Products => _productsRepository,
        //        RepositoryType.Users=>_usersRepository,
        //        _ => throw new NotImplementedException(),
        //    };
        //}




    }
}
