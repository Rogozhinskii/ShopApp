using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using ShopLibrary.Services.Interfaces;

namespace ShopUI.Services.Interfaces
{   
    public interface IRepositoryManager
    {
        List<object> Repositories { get; }
        public object GetRepository(RepositoryType repositoryType);



    }
}
