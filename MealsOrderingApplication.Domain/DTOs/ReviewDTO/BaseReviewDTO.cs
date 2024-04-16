using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ReviewDTO
{
    public class BaseReviewDTO : BaseDTO
    {
        [Required(ErrorMessage = "The Stars field is required.")]
        [Range(1, 5, ErrorMessage = "The Stars must be between 1 to 5.")]
        public ushort Stars { get; set; } = default!;

        [Required(ErrorMessage = "The Comment field is required.")]
        [StringLength(1_000, ErrorMessage = "The Description field must be less than 1000 characters.")]
        public string Comment { get; set; } = default!;
    }
}
