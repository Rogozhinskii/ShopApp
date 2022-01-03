using System.Data.Common;

namespace ShopLibrary.Services.Interfaces
{
    public interface IProviderFactoryService
    {   
        DbProviderFactory GetFactory(DatabaseType databaseType);
        DbProviderFactory GetFactory(string providerName);
        public void RegisterFactory(string assemblyName, DbProviderFactory dbProviderFactory);
    }
}
