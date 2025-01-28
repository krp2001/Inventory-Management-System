using InventoryProject.ViewModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryProject.Models
{
    public class Product

    {
        
        public string ProductId { get; set; }

        [StringLength(6)]

        public string ProductCode { get; set; }

        [Required]
        [StringLength(75)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int ProductQuantity { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        //public List<SelectListItem> ItemList { get; set; }

        public ICollection<ItemViewModel> items { get; set; }


    }
}
