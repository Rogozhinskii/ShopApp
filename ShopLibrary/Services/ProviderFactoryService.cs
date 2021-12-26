using ShopLibrary.Services.Interfaces;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ShopLibrary.Services
{
    public class ProviderFactoryService : IProviderFactoryService
    {
        private const string SQLProvider = "System.Data.SqlClient";
        private const string OleProvider = "System.Data.OleDb";
        public DbProviderFactory OleFactory => DbProviderFactories.GetFactory(OleProvider);

        public DbProviderFactory SqlFactory => DbProviderFactories.GetFactory(SQLProvider);

        public ProviderFactoryService()
        {
            RegisterFactory();
        }

        public void RegisterFactory(string providerName, DbProviderFactory dbProviderFactory)
        {
            DbProviderFactories.RegisterFactory(providerName, dbProviderFactory);
        }



        public void RegisterFactory()
        {
            DbProviderFactories.RegisterFactory(SQLProvider, SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory(OleProvider, OleDbFactory.Instance);
        }

        public DbProviderFactory GetFactory(string providerName)=>
            DbProviderFactories.GetFactory(providerName);
    }
}
