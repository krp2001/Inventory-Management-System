using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryProject.ViewModel
{
    public class LoginViewModel 
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
