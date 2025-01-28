using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryProject.Models
{
    public class Cart
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        
        public bool IsPaid { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
