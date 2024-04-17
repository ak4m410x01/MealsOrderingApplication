using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AuthanticationDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        [StringLength(100, ErrorMessage = "The FirstName field must be less than 100 characters.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "The LastName field is required.")]
        [StringLength(100, ErrorMessage = "The LastName field must be less than 100 characters.")]
        public string LastName { get; set; } = default!;


        [Required(ErrorMessage = "The Email field is required.")]
        [StringLength(256, ErrorMessage = "The Email field must be less than 256 characters.")]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; } = default!;


        [Required(ErrorMessage = "The Username field is required.")]
        [StringLength(256, ErrorMessage = "The Username field must be less than 256 characters.")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "The ConfirmPassword field is required.")]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; } = default!;


        [Required(ErrorMessage = "The PhoneNumber field is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The PhoneNumber number must be exactly 11 digits.")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "The Location field is required.")]
        public string Location { get; set; } = default!;
    }
}
