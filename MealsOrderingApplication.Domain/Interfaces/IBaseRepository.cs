namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public Task<IQueryable<T>> GetAllAsync();

        public void Add(T entity);
        public Task AddAsync(T entity);

        public T? GetById(int id);
        public Task<T?> GetByIdAsync(int id);

        public T? GetById(string id);
        public Task<T?> GetByIdAsync(string id);

        public void Update(T entity);
        public Task UpdateAsync(T entity);

        public void Delete(T entity);
        public Task DeleteAsync(T entity);
    }
}
