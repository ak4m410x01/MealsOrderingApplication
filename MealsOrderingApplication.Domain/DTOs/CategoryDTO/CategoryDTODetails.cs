using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.CategoryDTO
{
    public class CategoryDTODetails : BaseCategoryDTO, IDetailsDTO
    {
        public int Id { get; set; }
    }
}
