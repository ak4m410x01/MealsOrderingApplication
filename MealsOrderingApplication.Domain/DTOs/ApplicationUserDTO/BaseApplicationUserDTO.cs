using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO
{
    public class BaseApplicationUserDTO : BaseDTO
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
    }
}
