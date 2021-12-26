using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services.Interfaces;

namespace ShopUI.Services.Interfaces
{
    public interface IRepositoryManager
    {
        IUsersService UsersService { get; }
        IRepository<Customer> CustomersRepository { get; }
        
    }
}
