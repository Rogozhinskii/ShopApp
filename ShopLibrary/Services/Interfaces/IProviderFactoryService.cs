using System.Data.Common;

namespace ShopLibrary.Services.Interfaces
{
    public interface IProviderFactoryService
    {
        public DbProviderFactory OleFactory { get; }
        public DbProviderFactory SqlFactory { get; }
        void RegisterFactory(string providerName, DbProviderFactory dbProviderFactory);
        DbProviderFactory GetFactory(string providerName);
    }
}
