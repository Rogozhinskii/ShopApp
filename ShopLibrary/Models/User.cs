namespace ShopLibrary.Models
{
    public class User:EntityBase
    {
        /// <summary>
        /// Имя пользователя (login)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Соленный пароль пользователя
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// Соленый и хешированный пароль
        /// </summary>
        public string SaltedHashedPassword { get; set; }
        
    }
}
