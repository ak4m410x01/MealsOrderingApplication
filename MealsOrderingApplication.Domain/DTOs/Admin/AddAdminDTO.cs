using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.Admin
{
    public class AddAdminDTO : BaseAdminDTO
    {
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
    }
}
