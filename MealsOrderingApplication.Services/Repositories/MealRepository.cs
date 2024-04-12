using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using System.Xml.Linq;

namespace MealsOrderingApplication.Services.Repositories
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository
    {
        public MealRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(Meal entity)
        {
            Product? product = _context.Set<Product>().Find(entity.Id);
            if (product is null)
                throw new NullReferenceException("Can't Find Product");

            _context.Set<Product>().Remove(product);
        }
        public override async Task DeleteAsync(Meal entity)
        {
            Product? product = await _context.Set<Product>().FindAsync(entity.Id);
            if (product is null)
                throw new NullReferenceException("Can't Find Product");

            await Task.FromResult(_context.Set<Product>().Remove(product));
        }
    }
}
