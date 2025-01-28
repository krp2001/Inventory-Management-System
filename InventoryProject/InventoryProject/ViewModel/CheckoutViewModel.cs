namespace InventoryProject.ViewModel
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            Products = new List<CartViewModel>();
            Price = new List<decimal>();
        }
        public List<decimal> Price { get; set; }
        public List<CartViewModel> Products { get; set; }
    }
}
