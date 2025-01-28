using InventoryProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;

namespace InventoryProject.Models
{
    public class Supplier
    {
        public string SupplierId { get; set; }

        [Required]
        [StringLength(6)]
        [Remote("IsSupplierCodeExists", "Admin", ErrorMessage = " Supplier code Already Exists")]

        public string SupplierCode { get; set; } = "";

        [Required]
        [MaxLength(75)]
        public string SupplierName { get; set; } = "";


        [Remote("IsEmailExists", "Admin", AdditionalFields = "Id", ErrorMessage = "Email Id Already Exists")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", 
            ErrorMessage = "E-Mail is not Valid")]
        [MaxLength(75)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not Valid")]
        public string SupplierEmailId { get; set; } = "";

        [MaxLength(75)]
        public string SupplierAddress { get; set; } = "";

        [MaxLength(75)]
        [Phone]
        public string SupplierPhoneNo { get; set; } = "";


    }
}
