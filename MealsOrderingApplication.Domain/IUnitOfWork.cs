using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public ICustomerRepository Customers { get; }
        public IAdminRepository Admins { get; }
        public IDriverRepository Drivers { get; }
        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public IMealRepository Meals { get; }
        public IDrinkRepository Drinks { get; }
        public IOrderRepository Orders { get; }
        public IReviewRepository Reviews { get; }

        public int Complete();
        public Task<int> CompleteAsync();
    }
}
