using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ReviewDTO
{
    public class AddReviewDTO : BaseReviewDTO, IAddDTO
    {
        [Required(ErrorMessage = "The CustomerId field is required.")]
        public string CustomerId { get; set; } = default!;

        [Required(ErrorMessage = "The ProductId field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The ProductId must be greater than 0.")]
        public int ProductId { get; set; } = default!;
    }

}
