using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository Categories { get; }
        public ICustomerRepository Customers { get; }
        public IAdminRepository Admins { get; }
        public IProductRepository Products { get; }
        public IMealRepository Meals { get; }
        public IDrinkRepository Drinks { get; }
        public IOrderRepository Orders { get; }
        public IReviewRepository Reviews { get; }

        public int Complete();
        public Task<int> CompleteAsync();
    }
}
