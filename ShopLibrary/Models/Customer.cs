namespace ShopLibrary.Models
{
    public class Customer:EntityBase
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
