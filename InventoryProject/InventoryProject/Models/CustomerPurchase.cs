using System.ComponentModel.DataAnnotations;

namespace InventoryProject.Models
{
    public class CustomerPurchase
    {
        [Key]
        public string PurchaseId { get; set; }
        public string CustomerId { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal PayableAmount { get; set; }
        public bool PurchaseStatus { get; set; }
    }
}
