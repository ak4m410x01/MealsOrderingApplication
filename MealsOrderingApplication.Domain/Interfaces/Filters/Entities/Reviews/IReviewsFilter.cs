using MealsOrderingApplication.Domain.Entities;

namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Reviews
{
    public interface IReviewsFilter
    {
        Task<IQueryable<Review>> FilterByStarsAsync(IQueryable<Review> reviews, int stars);
        Task<IQueryable<Review>> FilterByCustomerAsync(IQueryable<Review> reviews, string customerId);
        Task<IQueryable<Review>> FilterByProductAsync(IQueryable<Review> reviews, int productId);
    }
}
