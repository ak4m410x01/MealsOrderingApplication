using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public readonly ApplicationDbContext _context;

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public virtual async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<T>().AsQueryable());
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public virtual async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_context.Set<T>().Update(entity));
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual async Task DeleteAsync(T entity)
        {
            await Task.FromResult(_context.Set<T>().Update(entity));
        }
    }
}
