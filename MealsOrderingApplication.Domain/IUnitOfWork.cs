using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository Categories { get; }
        public IMealRepository Meals { get; }
        public IDrinkRepository Drinks { get; }

        public int Complete();
        public Task<int> CompleteAsync();
    }
}
