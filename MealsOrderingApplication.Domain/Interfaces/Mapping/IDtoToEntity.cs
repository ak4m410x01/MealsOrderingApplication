using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.Interfaces.Mapping
{
    public interface IDtoToEntity<TEntity> where TEntity : class
    {
        Task<TEntity> MapAddDtoToEntity<TDto>(TDto dto) where TDto : IAddDTO;
        Task<TEntity> MapUpdateDtoToEntity<TDto>(TEntity entity, TDto dto) where TDto : IUpdateDTO;
    }
}
