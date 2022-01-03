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
            //var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ShopDb; Integrated Security =True";
            //ProviderFactoryService providerFactory = new();
            //var customerRep = new CustomersRepository(providerFactory.GetFactory(DatabaseType.SqlServer), connectionString);
            //var customers = Enumerable.Range(1, 10).Select(x => new Customer{
            //    Name = RandomData.GetRandomName(),
            //    Surname = RandomData.GetRandomSurname(),
            //    Patronymic = RandomData.GetRandomPatronymic(),
            //    PhoneNumber = RandomData.GetRandomPhoneNumber(),
            //}).ToList();
            //customers.ForEach(x => x.Email = RandomData.GetRandomEmail(x.Name+x.Surname, "@mail.ru"));
            //bool result=customerRep.InsertMany(customers);
            //Assert.IsTrue(result);

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