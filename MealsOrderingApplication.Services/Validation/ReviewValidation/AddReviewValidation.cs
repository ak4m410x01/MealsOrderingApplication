using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;

namespace MealsOrderingApplication.Services.Validation.ReviewValidation
{
    public class AddReviewValidation(IUnitOfWork unitOfWork) : BaseReviewValidation(unitOfWork), IAddReviewValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddReviewDTO addDto)
            {
                if (!(await IsProductExists(addDto.ProductId)))
                    return "Invalid ProductId";

                if (!(await IsCustomerExists(addDto.CustomerId)))
                    return "Invalid CustomerId";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddReviewDTO.");
        }
    }
}
