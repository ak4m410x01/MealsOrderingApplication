using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Services.IServices.Response;

namespace MealsOrderingApplication.Services
{
    public class UnitOfWork(
        ApplicationDbContext context,
        ICategoryRepository categories,
        IMealRepository meals,
        IDrinkRepository drinks,
        IProductRepository products,
        ICustomerRepository customers,
        IAdminRepository admins,
        IOrderRepository orders,
        IReviewRepository reviews) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        public ICategoryRepository Categories { get; private set; } = categories;
        public IProductRepository Products { get; private set; } = products;
        public IMealRepository Meals { get; private set; } = meals;
        public IDrinkRepository Drinks { get; private set; } = drinks;
        public ICustomerRepository Customers { get; private set; } = customers;
        public IAdminRepository Admins { get; private set; } = admins;
        public IOrderRepository Orders { get; private set; } = orders;
        public IReviewRepository Reviews { get; private set; } = reviews;

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
            GC.SuppressFinalize(this);
        }
    }
}
