using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain;

namespace MealsOrderingApplication.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
