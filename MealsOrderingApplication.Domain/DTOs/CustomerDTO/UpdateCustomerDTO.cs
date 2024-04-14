using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.CustomerDTO
{
    public class UpdateCustomerDTO : BaseCustomerDTO, IUpdateDTO
    {
        [StringLength(100, ErrorMessage = "The FirstName field must be less than 100 characters.")]
        public new string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "The LastName field must be less than 100 characters.")]
        public new string? LastName { get; set; }


        [StringLength(256, ErrorMessage = "The Email field must be less than 256 characters.")]
        public new string? Email { get; set; }

        [StringLength(256, ErrorMessage = "The Username field must be less than 256 characters.")]
        public new string? Username { get; set; }
        public string? Password { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "The PhoneNumber number must be exactly 11 digits.")]
        public new string? PhoneNumber { get; set; }
        public new string? Location { get; set; }
    }
}
