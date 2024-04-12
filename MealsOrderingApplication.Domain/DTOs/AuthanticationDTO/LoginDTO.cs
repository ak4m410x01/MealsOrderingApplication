using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AuthanticationDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
    }
}
