using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.Interfaces.Validations
{
    public interface IUpdateValidation : IBaseValidation
    {
        Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO;
    }
}
