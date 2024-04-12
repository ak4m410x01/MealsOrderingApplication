

using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.CategoryDTO
{
    public class BaseCategoryDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
