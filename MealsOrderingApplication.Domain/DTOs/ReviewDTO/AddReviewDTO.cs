using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ReviewDTO
{
    public class AddReviewDTO : BaseReviewDTO, IAddDTO
    {
        [Required(ErrorMessage = "The CustomerId field is required.")]
        public string CustomerId { get; set; } = default!;
    }

}
