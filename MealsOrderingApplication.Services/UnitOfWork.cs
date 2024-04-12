using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categories,
            IMealRepository meals, IDrinkRepository drinks,
            IProductRepository products)
        {
            _context = context;
            Categories = categories;
            Meals = meals;
            Drinks = drinks;
            Products = products;
        }

        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IMealRepository Meals { get; private set; }
        public IDrinkRepository Drinks { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
