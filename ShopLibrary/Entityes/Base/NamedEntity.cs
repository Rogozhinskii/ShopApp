using System.ComponentModel.DataAnnotations;

namespace ShopLibrary.Entityes.Base
{
    public class NamedEntity:Entity
    {
        [Required]
        public string Name { get; set; }
    }
}
