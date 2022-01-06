namespace ShopLibrary.Models
{
    /// <summary>
    /// Модель покупателя
    /// </summary>
    public class Customer:EntityBase,ICloneable
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
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

        public override string ToString() =>
            $"<{Id}><{Surname}><{Name}><{Patronymic}><{PhoneNumber}><{Email}>";
        public override bool Equals(object? obj) =>
            obj.ToString() == this.ToString();
        public override int GetHashCode() =>
            this.ToString().GetHashCode();
        /// <summary>
        /// Возвращает копию объекта
        /// </summary>
        /// <returns></returns>
        public object Clone() => new Customer
        {
            Id = Id,
            Email = Email,
            Name = Name,
            PhoneNumber = PhoneNumber,
            Surname = Surname,
            Patronymic =Patronymic
        };
    }
}
