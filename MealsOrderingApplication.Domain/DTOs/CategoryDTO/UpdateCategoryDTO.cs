using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO : BaseCategoryDTO, IUpdateDTO
    {
        public new string? Name { get; set; }
    }
}
