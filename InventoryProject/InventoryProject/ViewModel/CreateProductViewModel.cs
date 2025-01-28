using InventoryProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryProject.ViewModel
{
    public class CreateProductViewModel
    {
        
        [Key]
        public string? ProductId { get; set; }

        [StringLength(6)]
        [Remote("IsProductCodeExists", "Admin", ErrorMessage = " Product code Already Exists")]

        public string ProductCode { get; set; }

        [Required]
        [StringLength(75)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int ProductQuantity { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        [NotMapped]
        public ICollection<SelectListItem>? ItemsForDropdown { set; get; }

        public List<int> ItemIds { get; set; }
        

    }
}
