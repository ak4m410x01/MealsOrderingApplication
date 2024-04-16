using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;

namespace MealsOrderingApplication.Services.Validation.ReviewValidation
{
    public class UpdateReviewValidation : BaseReviewValidation, IUpdateReviewValidation
    {
        public UpdateReviewValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateReviewDTO addDto)
            {

                return await Task.FromResult(string.Empty);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateReviewDTO.");
        }
    }
}
