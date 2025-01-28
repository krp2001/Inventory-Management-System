using InventoryProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryProject.ViewModel
{
    public class ItemViewModel
    {
        [Key]
        public int ItemId { get; set; }
        
        [Required] 
        [StringLength(25)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Supplier")]
        public string? SupplierId { get; set; }
        
        [NotMapped]
        public ICollection<SelectListItem>? Suppliers { set; get; }

        

    }
}
