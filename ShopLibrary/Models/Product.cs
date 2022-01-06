namespace ShopLibrary.Models
{
    /// <summary>
    /// Модель продукта покупателя
    /// </summary>
    public class Product:EntityBase,ICloneable
    {
        public string Email { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        public int ProductCode { get; set; }
        /// <summary>
        /// Описание продукта
        /// </summary>
        public string Description { get; set; }

        public override string ToString()=>
            $"<{Id}><{Email}><{ProductCode}><{Description}>";

        public override bool Equals(object? obj)=>
            obj.ToString()==this.ToString();

        public override int GetHashCode() =>
            ToString().GetHashCode();

        /// <summary>
        /// Возвращает копию объекта
        /// </summary>
        /// <returns></returns>
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
