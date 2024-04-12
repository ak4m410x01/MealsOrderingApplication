using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<Category> GetAll()
        {
            return base.GetAll().OrderBy(c => c.Name);
        }
        public override async Task<IQueryable<Category>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<Category>().AsQueryable().OrderBy(c => c.Name));
        }
    }
}
