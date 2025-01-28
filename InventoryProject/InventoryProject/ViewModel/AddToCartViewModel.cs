using System.ComponentModel.DataAnnotations;

namespace InventoryProject.ViewModel
{
    public class AddToCartViewModel
    {
        public string ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        [Display(Name ="Available Quantity")]
        public int AvailableQuantity { get; set; }
        public int Quantity { get; set; }
    }
}