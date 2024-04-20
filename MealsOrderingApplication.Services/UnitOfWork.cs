using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Services.IServices.Response;

namespace MealsOrderingApplication.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categories,
            IMealRepository meals,
            IDrinkRepository drinks,
            IProductRepository products,
            ICustomerRepository customers,
            IAdminRepository admins,
            IDriverRepository drivers,
            IOrderRepository orders,
            IReviewRepository reviews)
        {
            _context = context;
            Categories = categories;
            Products = products;
            Meals = meals;
            Drinks = drinks;
            Customers = customers;
            Admins = admins;
            Drivers = drivers;
            Orders = orders;
            Reviews = reviews;
        }

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IMealRepository Meals { get; private set; }
        public IDrinkRepository Drinks { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IDriverRepository Drivers { get; private set; }
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
            GC.SuppressFinalize(this);
        }
    }
}
