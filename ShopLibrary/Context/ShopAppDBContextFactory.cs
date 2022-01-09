using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace ShopLibrary.Context
{
    public class ShopAppDBContextFactory : IDesignTimeDbContextFactory<ShopAppDB>
    {
        private readonly ConnectionStringSettings connectionStringSettings;

        public ShopAppDBContextFactory(ConnectionStringSettings connectionStringSettings)
        {
            this.connectionStringSettings = connectionStringSettings??throw new ArgumentNullException(nameof(connectionStringSettings));
        }
        public ShopAppDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopAppDB>();
            switch (connectionStringSettings.Name)
            {
                case "MSSQL":
                    optionsBuilder.UseSqlServer(connectionStringSettings.ConnectionString);
                    break;
                default: throw new InvalidOperationException("Не распознано имя строки подключения");
            }            
            return new ShopAppDB(optionsBuilder.Options);
        }
    }
}
