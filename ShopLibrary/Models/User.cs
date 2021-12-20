namespace ShopLibrary.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        /// <summary>
        /// Соленый и хешированный пароль
        /// </summary>
        public string SaltedHashedPassword { get; set; }
    }
}
