using Microsoft.EntityFrameworkCore;
using ShopLibrary.Context;
using ShopLibrary.Entityes;

namespace ShopLibrary
{
    internal class CustomersRepository : DBRepository<Customer>
    {
        public override IQueryable<Customer> Items => base.Items.Include(c=>c.Products);
        public CustomersRepository(ShopAppDB context) : base(context) { }
    }
}
