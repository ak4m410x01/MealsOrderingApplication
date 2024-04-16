using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.OrderDTO
{
    public class AddOrderDTO : BaseOrderDTO, IAddDTO
    {
        [Required(ErrorMessage = "The CustomerId field is required.")]
        public string CustomerId { get; set; } = default!;
    }
}
