using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.Customer
{
    public class BaseCustomerDTO
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "The Phone field is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The Phone number must be exactly 11 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Location field is required.")]
        public string Location { get; set; }
    }
}
