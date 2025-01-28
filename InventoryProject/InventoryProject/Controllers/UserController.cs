using InventoryProject.Data;
using InventoryProject.Migrations;
using InventoryProject.Models;
using InventoryProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace InventoryProject.Controllers
{
    [Authorize(Roles = "User")]

    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(AppDbContext context, UserManager<IdentityUser> userManager)
        {

            _context = context;
            this.userManager = userManager;
        }


        public IActionResult Home(string Name = "")
        {
            List<Product> Products;
            if (Name != "" && Name != null)
            {
                Products = _context.Products.Where(p => p.ProductName.Contains(Name)).ToList();
            }
            else
                Products = _context.Products.ToList();
            return View(Products);
        }


        [HttpGet]
        public async Task<IActionResult> AddToCart(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            var model = new AddToCartViewModel()
            {
                ProductId = id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Price = product.ProductPrice,
                AvailableQuantity = product.ProductQuantity,

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

            if (model.Quantity <= 0)
            {
                ModelState.AddModelError(string.Empty, "Enter Valid Quantity");
                model.ProductName = product.ProductName;
                model.ProductCode = product.ProductCode;
                model.AvailableQuantity = product.ProductQuantity;
                model.Price = product.ProductPrice;
                return View(model);
            }
            else if (product.ProductQuantity < model.Quantity)
            {
                ModelState.AddModelError(string.Empty, "Not Enough Quantity Available");
                model.ProductName = product.ProductName;
                model.ProductCode = product.ProductCode;
                model.AvailableQuantity = product.ProductQuantity;
                model.Price = product.ProductPrice;
                return View(model);
            }


            if (cart == null)
            {
                var newCart = new Cart()
                {
                    ProductId = product.ProductId,
                    UserId = user.Id,
                    Name = product.ProductName,
                    Quantity = model.Quantity,
                    IsPaid = false
                };
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
            }
            else
            {
                if(cart.IsPaid == true)
                {
                    cart.IsPaid = false;
                    cart.Quantity = model.Quantity;
                };
                cart.IsPaid = false;
                cart.Quantity = cart.Quantity + model.Quantity;
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Home", "User");
        }
        public Product getDetailProduct(int id)
        {
            var product = _context.Products.Find(id);
            return product;
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var carts = _context.Carts
                .Where(x => x.UserId == user.Id && x.IsPaid == false);

            var model = new List<CartViewModel>();

            foreach (var cart in carts)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cart.ProductId);
                decimal price = cart.Quantity * product.ProductPrice;
                var cartViewModel = new CartViewModel()
                {
                    ProductId = cart.ProductId,
                    ProductName = cart.Name,
                    Quantity = cart.Quantity,
                    Price = price
                };
                model.Add(cartViewModel);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CartHistory()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var carts = _context.Carts
                .Where(x => x.UserId == user.Id && x.IsPaid == true);

            var model = new List<CartViewModel>();

            foreach (var cart in carts)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cart.ProductId);
                decimal price = cart.Quantity * product.ProductPrice;
                var cartViewModel = new CartViewModel()
                {
                    ProductId = cart.ProductId,
                    ProductName = cart.Name,
                    Quantity = cart.Quantity,
                    Price = price
                };
                model.Add(cartViewModel);
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> RemoveCart(string ProductId)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == user.Id && x.ProductId == ProductId);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var carts = _context.Carts.Where(x => x.UserId == user.Id && x.IsPaid == false);
            var cartList = new List<CartViewModel>();
            var prices = new List<decimal>();
            foreach (var cart in carts)
            {
                cart.IsPaid = true;
                _context.Carts.Update(cart);
                var product = _context.Products.FirstOrDefault(x => x.ProductId == cart.ProductId);
                var cartViewModel = new CartViewModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = cart.Quantity,
                    Price = cart.Quantity * product.ProductPrice
                };
                cartList.Add(cartViewModel);
                prices.Add(product.ProductPrice);
            }
            await _context.SaveChangesAsync();

            var model = new CheckoutViewModel()
            {
                Price = prices,
                Products = cartList
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmPurchase(string total)
        {
            var id = GeneratePurchaseId();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var carts = _context.Carts.Where(x => x.UserId == user.Id);
            var purchase = new CustomerPurchase()
            {
                PurchaseId = id,
                PayableAmount = Convert.ToDecimal(total),
                CustomerId = user.Id,
                PurchaseStatus = false,
                TimeStamp = DateTime.Now
            };
            foreach (var cart in carts)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cart.ProductId);
                product.ProductQuantity -= cart.Quantity;
                _context.Products.Update(product);
            }
            _context.CustomerPurchase.Add(purchase);

            await _context.SaveChangesAsync();
            return View(purchase);
        }

        public string GeneratePurchaseId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);

        }


    }
}

