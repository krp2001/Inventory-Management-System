using System.ComponentModel.DataAnnotations;

namespace InventoryProject.ViewModel
{
    public class DeleteViewModel
    {

        [Key]
        public int ItemId { get ; set; }

        [Required]
        [StringLength(25)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
