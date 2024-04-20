using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.DriverDTO
{
    public class AddDriverDTO : BaseDriverDTO, IAddDTO
    {
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "The ConfirmPassword field is required.")]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
