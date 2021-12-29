namespace ShopLibrary.Models
{
    public class Product:EntityBase
    {
        public string Email { get; set; }
        public int ProductCode { get; set; }
        public string Description { get; set; }
    }
}
