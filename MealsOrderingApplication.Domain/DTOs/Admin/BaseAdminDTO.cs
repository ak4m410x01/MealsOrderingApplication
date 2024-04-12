using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.Admin
{
    public class BaseAdminDTO
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }
    }
}
