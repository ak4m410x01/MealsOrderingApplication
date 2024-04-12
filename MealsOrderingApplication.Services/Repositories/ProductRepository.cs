using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
