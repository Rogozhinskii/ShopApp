using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShopLibrary.Context
{
    internal class ShopAppDBContextFactory : IDesignTimeDbContextFactory<ShopAppDB>
    {
        public ShopAppDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopAppDB>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ShopDb; Integrated Security =True");
            return new ShopAppDB(optionsBuilder.Options);
        }
    }
}
