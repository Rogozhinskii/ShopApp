using ShopLibrary.Services.Interfaces;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ShopLibrary.Services
{
    public static class ProviderFactoryService  //IProviderFactoryService
    {
        //private const string SQLProvider = "Microsoft.Data.SqlClient";
        private const string SQLProvider = "System.Data.SqlClient";
        private const string OleProvider = "System.Data.OleDb";

        static ProviderFactoryService()
        {
            RegisterFactories();
        }

        public static void RegisterFactory(string assemblyName, DbProviderFactory dbProviderFactory)
        {
            DbProviderFactories.RegisterFactory(assemblyName, dbProviderFactory);
        }

        public static DbProviderFactory GetFactory(DatabaseType databaseType)
        {
            return databaseType switch
            {
                DatabaseType.SqlServer => DbProviderFactories.GetFactory(SQLProvider),
                DatabaseType.MsAccess => DbProviderFactories.GetFactory(OleProvider),
                _ => throw new NotImplementedException()
            };
        }

        public static DbProviderFactory GetFactory(string providerName) =>
            DbProviderFactories.GetFactory(providerName);

        private static void RegisterFactories()
        {
            DbProviderFactories.RegisterFactory(SQLProvider, SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory(OleProvider, OleDbFactory.Instance);
        }

    }
}
