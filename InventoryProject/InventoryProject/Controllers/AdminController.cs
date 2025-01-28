using InventoryProject.Data;
using InventoryProject.Models;
using InventoryProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace InventoryProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, AppDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;

        }

        #region Product
        public IActionResult ProductItem(string Name = "")
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


        public IActionResult CreateProduct()
        {
            CreateProductViewModel vm = new CreateProductViewModel();
            vm.ItemsForDropdown = _context.Items.Select(a => new SelectListItem()
            {
                Value = a.ItemId.ToString(),
                Text = a.ItemName
            }).ToList();
            return View(vm);

        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = Guid.NewGuid().ToString();
                    var product = new Product()
                    {
                        ProductId = id,
                        ProductCode = model.ProductCode,
                        ProductName = model.ProductName,
                        ProductPrice = model.ProductPrice,
                        ProductQuantity = model.ProductQuantity

                    };
                    _context.Products.Add(product);
                    for (int i = 0; i < model.ItemIds.Count; i++)
                    {
                        var itemProduct = new ItemProduct()
                        {
                            ItemId = model.ItemIds[i],
                            ProductId = id
                        };
                        _context.ItemProducts.Add(itemProduct);
                    }
                    _context.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("ProductItem", "Admin");
            }
            return View();
        }


        [HttpGet]
        public IActionResult EditProduct(string Id)
        {
            var products = from i in _context.Products where i.ProductId == Id select i;
            foreach (var product in products)
            {
                return View(product);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            try
            {
                _context.Products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("ProductItem", "Admin");
        }

        [HttpGet]
        public IActionResult DetailsProduct(string Id)
        {
            var products = from i in _context.Products where i.ProductId == Id select i;
            foreach (var product in products)
            {
                return View(product);
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteProduct(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Find(Id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);


        }
        [HttpPost]
        public IActionResult DeleteProduct(Product model)
        {
            var product = _context.Products.Find(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("ProductItem", "Admin");
        }




        #endregion


        #region admin
        public async Task<IActionResult> AdminHome()
        {
            List<CustomerPurchase> purchases = _context.CustomerPurchase.ToList();
            var names = new List<string>();
            foreach (var purchase in purchases)
            {
                var user = await userManager.FindByIdAsync(purchase.CustomerId);
                names.Add(user.UserName);
            }
            var model = new AdminHomeViewModel()
            {
                purchases = purchases,
                customerNames = names
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> paid(string Id)
        {
            var purchase = await _context.CustomerPurchase.FirstOrDefaultAsync(x => x.PurchaseId == Id);
            purchase.PurchaseStatus = true;

            _context.CustomerPurchase.Update(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminHome");
        }

        [HttpGet]
        public async Task<IActionResult> Unpaid(string Id)
        {
            var purchase = await _context.CustomerPurchase.FirstOrDefaultAsync(x => x.PurchaseId == Id);
            purchase.PurchaseStatus = false;

            _context.CustomerPurchase.Update(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminHome");
        }

        #endregion


        #region item
        public IActionResult Home(string Name = "")
        {
            List<ItemViewModel> Items;

            if (Name != "" && Name != null)
            {
                Items = _context.Items.Where(p => p.ItemName.Contains(Name)).ToList();
            }
            else
                Items = _context.Items.ToList();
            return View(Items);
        }


        public IActionResult CreateItem()
        {

            ItemViewModel Item = new ItemViewModel();
            Item.Suppliers = _context.Suppliers.Select(a => new SelectListItem()
            {
                Value = a.SupplierId,
                Text = a.SupplierName
            }).ToList();
            return View(Item);
        }

        [HttpPost]
        public IActionResult CreateItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Items.Add(model);
                    _context.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction(nameof(Home));
            }
            return View();
        }
        [HttpGet]
        public IActionResult DetailsItem(int Id)
        {
            var items = from i in _context.Items where i.ItemId == Id select i;
            foreach (var item in items)
            {
                return View(item);
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditItem(int Id)
        {
            var items = _context.Items.FirstOrDefault(x => x.ItemId == Id);
            var model = new ItemViewModel()
            {
                ItemId = Id,
                ItemName = items.ItemName,
                Quantity = items.Quantity,
                Price = items.Price,
                SupplierId = items.SupplierId
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult EditItem(ItemViewModel model)
        {
            try
            {

                _context.Items.Update(model);
                _context.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction(nameof(Home));
        }


        [HttpGet]
        public IActionResult DeleteItem(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var items = _context.Items.Find(Id);
            var model = new DeleteViewModel()
            {
                ItemId = items.ItemId,
                ItemName = items.ItemName,
                Price = items.Price,
                Quantity = items.Quantity
            };
            if (items == null)
            {
                return NotFound();
            }
            return View(model);


        }

        [HttpPost]
        public IActionResult DeleteItem(DeleteViewModel model)
        {
            var item = _context.Items.Find(model.ItemId);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Home", "Admin");
        }



        #endregion

        #region Purchase
        public IActionResult PurchaseItem()
        {
            return View();
        }
        #endregion


        #region Supplier

        public IActionResult SupplierHome(string Name = "")
        {
            List<Supplier> Suppliers;
            if (Name != "" && Name != null)
            {
                Suppliers = _context.Suppliers.Where(p => p.SupplierName.Contains(Name)).ToList();
            }
            else
                Suppliers = _context.Suppliers.ToList();
            return View(Suppliers);
        }


        public IActionResult CreateSupplier()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateSupplier(CreateSupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplier = new Supplier()
                    {
                        SupplierId = Guid.NewGuid().ToString(),
                        SupplierCode = model.SupplierCode,
                        SupplierName = model.SupplierName,
                        SupplierAddress = model.SupplierAddress,
                        SupplierEmailId = model.SupplierEmailId,
                        SupplierPhoneNo = model.SupplierPhoneNo

                    };
                    _context.Suppliers.Add(supplier);
                    _context.SaveChanges();
                }


                catch
                {

                }
                return RedirectToAction("SupplierHome", "Admin");
            }
            return View();

        }

        [HttpGet]
        public IActionResult EditSupplier(string Id)
        {
            var suppliers = from i in _context.Suppliers where i.SupplierId == Id select i;
            foreach (var supplier in suppliers)
            {
                return View(supplier);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Attach(supplier);
                _context.Entry(supplier).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("SupplierHome", "Admin");
        }

        [HttpGet]
        public IActionResult DetailsSupplier(string Id)
        {
            var suppliers = from i in _context.Suppliers where i.SupplierId == Id select i;
            foreach (var supplier in suppliers)
            {
                return View(supplier);
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteSupplier(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var supplier = _context.Suppliers.Find(Id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);


        }
        [HttpPost]
        public IActionResult DeleteSupplier(Supplier model)
        {
            var supplier = _context.Suppliers.Find(model.SupplierId);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return RedirectToAction("SupplierHome", "Admin");
        }
        #endregion

        #region Roles
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListRoles");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
        #endregion

        #region Validation

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsProductCodeExists(string productcode)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductCode == productcode);

            if (product == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Product Code {productcode} is already in use.");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsSupplierCodeExists(string suppliercode)
        {

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierCode == suppliercode);

            if (supplier == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Product Code {suppliercode} is already in use.");
            }

        }
        #endregion



    }
}


