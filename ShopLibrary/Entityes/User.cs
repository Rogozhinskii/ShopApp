using ShopLibrary.Entityes.Base;
using System.ComponentModel.DataAnnotations;

namespace ShopLibrary.Entityes
{
    public class User:NamedEntity
    {
        /// <summary>
        /// Соленный пароль пользователя
        /// </summary>
        /// 
        [Required]
        public string Salt { get; set; }
        /// <summary>
        /// Соленый и хешированный пароль
        /// </summary>
        /// 
        [Required]
        public string SaltedHashedPassword { get; set; }
    }
}
