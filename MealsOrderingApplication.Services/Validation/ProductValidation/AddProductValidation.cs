using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.ProductValidation
{
    public class AddProductValidation : BaseProductValidation, IAddProductValidation
    {
        public AddProductValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddProductDTO addDto)
            {
                if ((await _unitOfWork.Categories.GetByIdAsync(addDto.CategoryId)) is null)
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddProductDTO.");
        }
    }
}
