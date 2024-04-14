using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Mapping;

namespace MealsOrderingApplication.Services.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDtoToEntity<TEntity> where TEntity : class
    {
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;


        // Retrieve all Entities from Repository
        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }


        // Retrive Entity from Repository By Id
        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        // Add Entity to Repository
        public virtual async Task<TEntity> AddAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            TEntity entity = await MapAddDtoToEntity<TDto>(dto);

            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }


        // Update Entity in Repository
        public virtual async Task<TEntity> UpdateAsync<TDto>(TEntity entity, TDto dto) where TDto : IUpdateDTO
        {
            entity = await MapUpdateDtoToEntity<TDto>(entity, dto);

            await Task.FromResult(_context.Set<TEntity>().Update(entity));
            return await Task.FromResult(entity);
        }

        // Delete Entity from Repository
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(_context.Set<TEntity>().Remove(entity));
        }



        // Map Add Dto To Entity
        public abstract Task<TEntity> MapAddDtoToEntity<TDto>(TDto dto) where TDto : IAddDTO;

        // Map Update Dto To Entity
        public abstract Task<TEntity> MapUpdateDtoToEntity<TDto>(TEntity entity, TDto dto) where TDto : IUpdateDTO;

    }
}
