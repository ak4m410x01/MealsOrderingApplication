using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public int Complete();
        public Task<int> CompleteAsync();
    }
}
