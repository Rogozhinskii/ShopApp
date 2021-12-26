using ShopLibrary.DAL.interfaces;

namespace ShopLibrary.Models
{
    public class User:EntityBase
    {       
        public string Name { get; set; }
        public string Salt { get; set; }
        /// <summary>
        /// Соленый и хешированный пароль
        /// </summary>
        public string SaltedHashedPassword { get; set; }
        
    }
}
