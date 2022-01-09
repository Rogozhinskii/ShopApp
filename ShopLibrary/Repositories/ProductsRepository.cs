using Microsoft.EntityFrameworkCore;
using ShopLibrary.Context;
using ShopLibrary.Entityes;

namespace ShopLibrary
{
    public class ProductsRepository : DBRepository<Product>
    {
        public override IQueryable<Product> Items => base.Items.Include(c=>c.Customer);
        public ProductsRepository(ShopAppDB context) : base(context) { }
    }
}
