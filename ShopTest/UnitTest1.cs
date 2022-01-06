using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopLibrary.DAL.Repositories;
using ShopLibrary.Models;
using ShopLibrary.Models.Data;
using ShopLibrary.Services;
using ShopUI.Services;
using System;
using System.Configuration;
using System.Linq;


namespace ShopTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InsertManyCustomers()
        {
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ShopDb; Integrated Security =True";
            //ProviderFactoryService providerFactory = new();
            var customerRep = new CustomersRepository(ProviderFactoryService.GetFactory(DatabaseType.SqlServer), connectionString);
            var customer = new Customer
            {
                Email = "sdfsfa",
                Name = "dsfsadf",
                Surname = "dfgdgsdf",
                Patronymic = "sfdwefw",
                PhoneNumber = "sdfadas"
            };
            var result=customerRep.Insert(customer);
           
           

        }

        [TestMethod]
        public void InsertManyProducts()
        {
            //Random rnd = new();
            //var connectionString1 = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ShopDb; Integrated Security =True";
            //ProviderFactoryService providerFactory = new();
            ////var customerRep = new CustomersRepository(providerFactory.GetFactory(DatabaseType.SqlServer), connectionString1);
            ////var customers = customerRep.GetAll();
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\..\\ShopLibrary\\Access\\ShopDb.accdb";
            var productsRepository = new ProductsRepository(ProviderFactoryService.GetFactory(DatabaseType.MsAccess), connectionString);
                        
            //var result = productsRepository.GetAll("id", 1);
            //Assert.IsTrue(result);
        }
    }
}