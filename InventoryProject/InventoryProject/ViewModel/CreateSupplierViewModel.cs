using InventoryProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryProject.ViewModel
{
    public class CreateSupplierViewModel
    {
      

        [Key]
        public string? SupplierId { get; set; }

        [Required]
        [StringLength(6)]

        public string SupplierCode { get; set; } 

        [Required]
        [MaxLength(75)]
        public string SupplierName { get; set; } 


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "E-Mail is not Valid")]
        [MaxLength(75)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not Valid")]
        public string SupplierEmailId { get; set; } 

        [MaxLength(75)]
        public string SupplierAddress { get; set; } 

        [MaxLength(75)]
        [Phone]
        public string SupplierPhoneNo { get; set; } 

       

    }
}
