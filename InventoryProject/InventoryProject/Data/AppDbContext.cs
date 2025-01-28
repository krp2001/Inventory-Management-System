using InventoryProject.Models;
using InventoryProject.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Login> Login  { get; set; }

        public DbSet<ItemViewModel> Items { get; set; }
    
        public DbSet<ItemProduct> ItemProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<CustomerPurchase> CustomerPurchase { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Cart>()
                .HasKey(x => new { x.ProductId, x.UserId });
           
            builder.Entity<ItemProduct>()
                .HasKey(x => new { x.ProductId, x.ItemId });

        }
    }
}

