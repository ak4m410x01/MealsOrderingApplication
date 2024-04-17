using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.ProductValidation
{
    public class AddProductValidation(IUnitOfWork unitOfWork) : BaseProductValidation(unitOfWork), IAddProductValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddProductDTO addDto)
            {
                if (!(await IsCategoryExists(addDto.CategoryId)))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddProductDTO.");
        }
    }
}
