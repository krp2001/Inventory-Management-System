using InventoryProject.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InventoryProject.ViewModel
{
    public class AdminHomeViewModel
    {
        public AdminHomeViewModel()
        {
            purchases = new List<CustomerPurchase>();
            customerNames = new List<string>();
        }
        public List<CustomerPurchase> purchases { get; set; }
        public List<string> customerNames { get; set; }
    }
}
