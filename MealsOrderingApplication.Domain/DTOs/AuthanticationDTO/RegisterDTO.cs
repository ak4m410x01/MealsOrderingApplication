using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AuthanticationDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; } = default!;


        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = default!;


        [Required(ErrorMessage = "The Phone field is required.")]
        public string Phone { get; set; } = default!;

        [Required(ErrorMessage = "The Location field is required.")]
        public string Location { get; set; } = default!;
    }
}
