using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.MealDTO
{
    public class UpdateMealDTO : BaseMealDTO
    {

        public new string? Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public new double? Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The CategoryId must be greater than 0.")]
        public new int? CategoryId { get; set; }
    }
}
