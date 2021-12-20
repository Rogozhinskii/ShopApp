using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopLibrary.Authentication;
using ShopLibrary.Models;
using ShopLibrary.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace ShopTest
{
    [TestClass]
    public class UnitTest1
    {
       [TestMethod]
        public void RegisterTest()
        {
            SqlConnectionStringBuilder connectionString = new()
            {
                DataSource = @"(localdb)\mssqllocaldb",
                InitialCatalog = "ShopDb",
                IntegratedSecurity = true,
                Pooling = true
            };            
            string providerName = "System.Data.SqlClient";
            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
            UsersService usersService = new UsersService(providerName,connectionString.ToString());
            Protector protector = new(usersService);
            var result=protector.Register("Admin", "Admin");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LogInTest()
        {
            SqlConnectionStringBuilder connectionString = new()
            {
                DataSource = @"(localdb)\mssqllocaldb",
                InitialCatalog = "ShopDb",
                IntegratedSecurity = true,
                Pooling = true
            };
            string providerName = "System.Data.SqlClient";
            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
            UsersService usersService = new UsersService(providerName, connectionString.ToString());
            Protector protector = new(usersService);
            var result = protector.LogIn("Admin", "Admin");
            Assert.IsTrue(result);
        }
    }
}