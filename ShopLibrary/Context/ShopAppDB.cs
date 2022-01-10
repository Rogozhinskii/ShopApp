using Microsoft.EntityFrameworkCore;
using ShopLibrary.Entityes;

namespace ShopLibrary.Context
{
    /// <summary>
    /// Описывает контекст подключения к источнику данных
    /// </summary>
    public class ShopAppDB:DbContext
    {
        /// <summary>
        /// Таблица Products в источнике данных
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Таблица Customer в источнике данных
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Таблица Users в источнике данных
        /// </summary>
        public DbSet<User> Users { get; set; }
        public ShopAppDB(DbContextOptions<ShopAppDB> options) : base(options) { }
    }
}
