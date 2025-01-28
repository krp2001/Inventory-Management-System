using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace InventoryProject.Models
{
    public class Login
    {
        [Key]
        public int Username { get; set; }
        public string Password { get; set; }
    }
}
