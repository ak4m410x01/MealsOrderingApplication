using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.AdminDTO
{
    public class AddAdminDTO : BaseAdminDTO, IAddDTO
    {
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; } = default!;
    }
}
