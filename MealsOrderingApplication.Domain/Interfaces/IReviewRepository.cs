using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Reviews;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>, IReviewsFilter
    {
    }
}
