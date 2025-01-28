//using InventoryProject.Data;
//using InventoryProject.Interfaces;
//using InventoryProject.Models;
//using Microsoft.EntityFrameworkCore;

//namespace InventoryProject.Repositories
//{
//    public class SupplierRepo : ISupplier
//    {
//        private readonly AppDbContext _context;

//        public SupplierRepo(AppDbContext context)
//        {
//            _context = context;
//        }
//        public Supplier Create(Supplier supplier)
//        {
//            _context.Suppliers.Add(supplier);
//            _context.SaveChanges();
//            return supplier;
//        }

//        public Supplier Delete(Supplier supplier)
//        {
//            _context.Suppliers.Attach(supplier);
//            _context.Entry(supplier).State = EntityState.Deleted;
//            _context.SaveChanges();
//            return supplier;
//        }

//        public Supplier Edit(Supplier supplier)
//        {
//            _context.Suppliers.Attach(supplier);
//            _context.Entry(supplier).State = EntityState.Modified;
//            _context.SaveChanges();
//            return supplier;
//        }

//        public Supplier GetItem(int id)
//        {
//            Supplier supplier = _context.Suppliers.Where(s => s.Id == id).FirstOrDefault();
//            return supplier;
//        }

//        public bool IsSupplierCodeExists(string code)
//        {

//            int ct = _context.Suppliers.Where(s => s.Code.ToLower() == code.ToLower()).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;

//        }
//        public bool IsSupplierCodeExists(string code, int Id)
//        {
//            int ct = _context.Suppliers.Where(s => s.Code.ToLower() == code.ToLower() && s.Id != Id).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;

//        }
//        public bool IsSupplierEmailExists(string email)
//        {
//            int ct = _context.Suppliers.Where(s => s.EmailId.ToLower() == email.ToLower()).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;

//        }
//        public bool IsSupplierEmailExists(string email, int Id)
//        {
//            int ct = _context.Suppliers.Where(s => s.EmailId.ToLower() == email.ToLower() && s.Id != Id).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;
//        }
//        public bool IsSupplierNameExists(string name)
//        {
//            int ct = _context.Suppliers.Where(s => s.Name.ToLower() == name.ToLower()).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;

//        }
//        public bool IsSupplierNameExists(string name, int Id)
//        {
//            int ct = _context.Suppliers.Where(s => s.Name.ToLower() == name.ToLower() && s.Id != Id).Count();
//            if (ct > 0)
//                return true;
//            else
//                return false;
//        }
//    }
//}

