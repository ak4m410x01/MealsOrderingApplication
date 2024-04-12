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

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<T>().AsQueryable());
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_context.Set<T>().Update(entity));
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            await Task.FromResult(_context.Set<T>().Update(entity));
        }
    }
}
