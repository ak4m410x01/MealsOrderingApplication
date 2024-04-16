using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.ReviewDTO
{
    public class ReviewDTODetails : BaseReviewDTO, IDetailsDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = default!;
        public int ProductId { get; set; } = default!;
    }

}
