using ShopLibrary.Entityes.Base;

namespace ShopLibrary.Entityes
{
    public class Product: Entity
    {
        public string Email { get => Customer.Email; set { }}

        /// <summary>
        /// Код продукта
        /// </summary>
        public int ProductCode { get; set; }
        /// <summary>
        /// Описание продукта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Владелец продукта
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}
