using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AuthanticationDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [StringLength(256, ErrorMessage = "The Email field must be less than 256 characters.")]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = default!;
    }
}
