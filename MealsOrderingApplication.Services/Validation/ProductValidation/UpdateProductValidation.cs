using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.ProductValidation
{
    public class UpdateProductValidation : BaseProductValidation, IUpdateProductValidation
    {
        public UpdateProductValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateProductDTO addDto)
            {
                if ((addDto.CategoryId is not null) && ((await _unitOfWork.Categories.GetByIdAsync(addDto.CategoryId)) is null))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateProductDTO.");
        }
    }
}
