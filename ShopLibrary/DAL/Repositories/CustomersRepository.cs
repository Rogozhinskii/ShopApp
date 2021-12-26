using ShopLibrary.DAO.interfaces;
using ShopLibrary.Models;
using System.Data.Common;

namespace ShopLibrary.DAL.Repositories
{
    public class CustomersRepository : Repository<Customer>
    {
        public CustomersRepository(DbProviderFactory factory, string connectionString) 
            : base(factory, connectionString)
        {
        }

        public override List<Customer> GetAll()
        {
            return base.GetAll();
        }
    }
}
