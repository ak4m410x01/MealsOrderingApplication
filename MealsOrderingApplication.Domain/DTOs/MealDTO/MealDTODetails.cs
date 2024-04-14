using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.MealDTO
{
    public class MealDTODetails : BaseMealDTO, IDetailsDTO
    {
        public int Id { get; set; }
    }
}
