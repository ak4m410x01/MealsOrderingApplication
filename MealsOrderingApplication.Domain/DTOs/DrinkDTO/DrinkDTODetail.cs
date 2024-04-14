using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.DrinkDTO
{
    public class DrinkDTODetails : BaseDrinkDTO, IDetailsDTO
    {
        public int Id { get; set; }
    }
}
