using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services.Interfaces;
using System.Configuration;

namespace ShopUI.Services.Interfaces
{
    public interface IRepositoryManager
    {
        List<object> Repositories { get; }
        IUsersService UsersService { get; }
        IRepository<Customer> CustomersRepository { get; }
        

    }
}
