using ShopLibrary.Entityes.Base;

namespace ShopLibrary.Entityes
{
    public class Customer:NamedEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }       
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Номер телефора
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Адрес почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Коллекция продуктов покупателя
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        public Customer()
        {
            Products=new List<Product>();
        }
        public override string ToString() =>
           $"<{Id}><{Surname}><{Name}><{Patronymic}><{PhoneNumber}><{Email}>";
    }
}





