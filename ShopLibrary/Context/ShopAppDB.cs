using Microsoft.EntityFrameworkCore;
using ShopLibrary.Entityes;

namespace ShopLibrary.Context
{
    public class ShopAppDB:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public ShopAppDB(DbContextOptions<ShopAppDB> options) : base(options) { }
    }
}
