using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ShopLibrary.Services
{
    public static class ProviderFactoryService  
    {
        #region пространства имен поставщиков 
        private const string SQLProvider = "System.Data.SqlClient";
        private const string OleProvider = "System.Data.OleDb";
        #endregion

        static ProviderFactoryService()
        {
            RegisterFactories();
        }

        /// <summary>
        /// Регистрирует провайдера и фабрику создания экземпляров методов поставщиков, реализующих источник данных
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="dbProviderFactory"></param>
        public static void RegisterFactory(string assemblyName, DbProviderFactory dbProviderFactory)
        {
            DbProviderFactories.RegisterFactory(assemblyName, dbProviderFactory);
        }

        /// <summary>
        /// Возвращает зарегистрированную фабрику поставщика по типу источника данных
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static DbProviderFactory GetFactory(DatabaseType databaseType)
        {
            return databaseType switch
            {
                DatabaseType.SqlServer => DbProviderFactories.GetFactory(SQLProvider),
                DatabaseType.MsAccess => DbProviderFactories.GetFactory(OleProvider),
                _ => throw new NotImplementedException()
            };
        }
                
        private static void RegisterFactories()
        {
            DbProviderFactories.RegisterFactory(SQLProvider, SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory(OleProvider, OleDbFactory.Instance);
        }

    }
}
