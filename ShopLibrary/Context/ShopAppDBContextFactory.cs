using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ShopLibrary.Context
{
    public class ShopAppDBContextFactory : IDesignTimeDbContextFactory<ShopAppDB>
    {
        
        public ShopAppDBContextFactory() {}

        /// <summary>
        /// Возвращает контекст подключения к источнику данных
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ShopAppDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopAppDB>();
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            var type = config["Type"];
            switch (type)
            {
                case "MSSQL":
                    optionsBuilder.UseSqlServer(config.GetConnectionString(type));
                    break;
                default:
                    throw new InvalidOperationException("Invalid DB Type");
            }
            return new ShopAppDB(optionsBuilder.Options);
        }
    }
}
