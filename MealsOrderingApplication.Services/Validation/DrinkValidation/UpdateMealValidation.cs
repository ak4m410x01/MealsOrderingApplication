using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DrinkDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace DrinksOrderingApplication.Services.Validation.DrinkValidation
{
    public class UpdateDrinkValidation(IUnitOfWork unitOfWork) : BaseDrinkValidation(unitOfWork), IUpdateDrinkValidation
    {
        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateDrinkDTO updateDto)
            {
                if ((updateDto.CategoryId is not null) && (!(await IsCategoryExistsAsync(updateDto.CategoryId ?? default))))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateDrinkDTO.");
        }
    }
}
