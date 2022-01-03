namespace ShopLibrary.Models
{
    public class Product:EntityBase
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

    }
}
