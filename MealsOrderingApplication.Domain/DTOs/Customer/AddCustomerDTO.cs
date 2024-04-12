using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.Customer
{
    public class AddCustomerDTO : BaseCustomerDTO
    {
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
    }
}
