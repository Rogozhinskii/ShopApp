using ShopLibrary.Services.Interfaces;

namespace ShopUI.Services.Interfaces
{
    public interface IRepositoryManager
    {
        IUsersService UsersService { get; }

        
    }
}
