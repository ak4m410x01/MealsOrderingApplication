using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;

namespace MealsOrderingApplication.Services.Validation.ReviewValidation
{
    public class AddReviewValidation : BaseReviewValidation, IAddReviewValidation
    {
        public AddReviewValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddReviewDTO addDto)
            {
                if ((await _unitOfWork.Products.GetByIdAsync(addDto.ProductId)) is null)
                    return "Invalid ProductId";

                if ((await _unitOfWork.Customers.GetByIdAsync(addDto.CustomerId)) is null)
                    return "Invalid CustomerId";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddReviewDTO.");
        }
    }
}
