using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopLibrary.Authentication;
using ShopLibrary.DAL;
using ShopLibrary.Models;
using ShopLibrary.Services;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
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
            UsersService usersService = new UsersService(providerName, connectionString.ToString());
            Protector protector = new(usersService);
            var result = protector.Register("Admin", "Admin");
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

        [TestMethod]
        public void AccessConnectionTest()
        {
            string providerName = "Microsoft.ACE.OLEDB.12.0";
            string path = @$"..\..\..\..\ShopLibrary\Access\ShopDb.accdb";
            DbProviderFactories.RegisterFactory(providerName, OleDbFactory.Instance);            
            var factory=DbProviderFactories.GetFactory(providerName);
            OleDbConnectionStringBuilder connectionStringBuilder = new()
            {
                Provider = providerName,
                DataSource = path
            };
          
            IDbConnection connection=factory.CreateConnection();
            connection.ConnectionString = connectionStringBuilder.ConnectionString;
            connection.Open();

            string sql = "Insert into Products (email,productId,productName)" +
                                            "values('rr@mail.ru','32','MSI мышь компьютерная')";
            IDbCommand cmd=factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            connection.Close();
        
        
        }

        [TestMethod]
        public void ProductsTest()
        {
            ProductsDAL productsDAL = new();
        }
    }
}