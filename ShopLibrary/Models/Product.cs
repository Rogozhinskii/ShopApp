namespace ShopLibrary.Models
{
    public class Product:EntityBase,ICloneable
    {
        public string Email { get; set; }
        public int ProductCode { get; set; }
        public string Description { get; set; }

        public override string ToString()=>
            $"<{Id}><{Email}><{ProductCode}><{Description}>";

        public override bool Equals(object? obj)=>
            obj.ToString()==this.ToString();

        public override int GetHashCode() =>
            ToString().GetHashCode();

        public object Clone()
        {
            return new Product
            {
                Id = Id,
                Email = Email,
                ProductCode = ProductCode,
                Description = Description
            };
        }
    }
}
