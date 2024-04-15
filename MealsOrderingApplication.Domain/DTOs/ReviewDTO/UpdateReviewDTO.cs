using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ReviewDTO
{
    public class UpdateReviewDTO : BaseReviewDTO, IUpdateDTO
    {
        [Range(1, 5, ErrorMessage = "The Stars must be between 1 to 5.")]
        public new ushort? Stars { get; set; }

        [StringLength(1_000, ErrorMessage = "The Description field must be less than 1000 characters.")]
        public new string? Comment { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The ProductId must be greater than 0.")]
        public new int? ProductId { get; set; }

    }

}
