using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.CustomerDTO
{
    public class BaseCustomerDTO : BaseApplicationUserDTO
    {
        [Required(ErrorMessage = "The PhoneNumber field is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The PhoneNumber number must be exactly 11 digits.")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "The Location field is required.")]
        public string Location { get; set; } = default!;
    }
}
