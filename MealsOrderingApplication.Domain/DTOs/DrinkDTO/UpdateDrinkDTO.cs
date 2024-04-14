using MealsOrderingApplication.Domain.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.DrinkDTO
{
    public class UpdateDrinkDTO : BaseDrinkDTO, IUpdateDTO
    {
        public new string? Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public new double? Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The CategoryId must be greater than 0.")]
        public new int? CategoryId { get; set; }
    }
}
