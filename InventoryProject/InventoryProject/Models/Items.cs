using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryProject.Models
{
    public class Items
    {
        [Key]
        public string ItemId { get; set; }

        [Required]
        [StringLength(25)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }

    }
}
