using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.Interfaces.Validations
{
    public interface IAddValidation : IBaseValidation
    {
        Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO;
    }
}
