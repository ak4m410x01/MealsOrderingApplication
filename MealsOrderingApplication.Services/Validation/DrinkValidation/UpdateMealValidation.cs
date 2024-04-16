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
            if (dto is UpdateDrinkDTO addDto)
            {
                if ((addDto.CategoryId is not null) && ((await _unitOfWork.Categories.GetByIdAsync(addDto.CategoryId)) is null))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateDrinkDTO.");
        }
    }
}
