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
            IMealRepository meals,
            IDrinkRepository drinks,
            IProductRepository products,
            ICustomerRepository customers,
            IAdminRepository admins,
            IOrderRepository orders,
            IReviewRepository reviews)
        {
            _context = context;
            Categories = categories;
            Meals = meals;
            Drinks = drinks;
            Products = products;
            Customers = customers;
            Admins = admins;
            Orders = orders;
            Reviews = reviews;
        }

        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IMealRepository Meals { get; private set; }
        public IDrinkRepository Drinks { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IReviewRepository Reviews { get; private set; }

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
