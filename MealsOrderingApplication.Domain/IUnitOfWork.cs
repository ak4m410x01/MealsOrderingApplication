using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository Categories { get; }

        public int Complete();
        public Task<int> CompleteAsync();
    }
}
