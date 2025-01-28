using System.ComponentModel.DataAnnotations;

namespace InventoryProject.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
