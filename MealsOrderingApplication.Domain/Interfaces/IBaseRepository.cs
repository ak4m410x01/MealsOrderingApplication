using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        // Retrieve all Entities from Repository
        Task<IQueryable<TEntity>> GetAllAsync();


        // Add Entity to Repository
        Task<TEntity> AddAsync<TDto>(TDto dto) where TDto : IAddDTO;


        // Retrive Entity from Repository By Id
        Task<TEntity?> GetByIdAsync(object id);


        // Update Entity in Repository
        Task<TEntity> UpdateAsync<TDto>(TEntity entity, TDto dto) where TDto : IUpdateDTO;


        // Delete Entity from Repository
        Task DeleteAsync(TEntity entity);
    }
}
