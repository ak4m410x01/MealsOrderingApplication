using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DrinkDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace DrinksOrderingApplication.Services.Validation.DrinkValidation
{
    public class AddDrinkValidation : BaseDrinkValidation, IAddDrinkValidation
    {
        public AddDrinkValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddDrinkDTO addDto)
            {
                if ((await _unitOfWork.Categories.GetByIdAsync(addDto.CategoryId)) is null)
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddDrinkDTO.");
        }
    }
}
