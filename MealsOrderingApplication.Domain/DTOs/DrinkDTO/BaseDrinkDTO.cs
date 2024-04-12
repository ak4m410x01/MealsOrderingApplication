using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.DrinkDTO
{
    public class BaseDrinkDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The CategoryId field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The CategoryId must be greater than 0.")]
        public int CategoryId { get; set; }
    }
}
