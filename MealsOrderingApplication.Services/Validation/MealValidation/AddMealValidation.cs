using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;
using MealsOrderingApplication.Services.Validation.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.MealValidation
{
    public class AddMealValidation : BaseProductValidation, IAddMealValidation
    {
        public AddMealValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddMealDTO addDto)
            {
                if (!(await IsCategoryExistsAsync(addDto.CategoryId)))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddMealDTO.");
        }
    }
}
