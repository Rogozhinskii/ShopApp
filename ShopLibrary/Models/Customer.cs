namespace ShopLibrary.Models
{
    public class Customer:EntityBase,ICloneable
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString() =>
            $"<{Id}><{Surname}><{Name}><{Patronymic}><{PhoneNumber}><{Email}>";
        public override bool Equals(object? obj) =>
            obj.ToString() == this.ToString();
        public override int GetHashCode() =>
            this.ToString().GetHashCode();

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
