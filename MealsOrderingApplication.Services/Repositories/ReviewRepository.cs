using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Reviews;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository, IReviewsFilter
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Review> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddReviewDTO addDto)
            {
                return await Task.FromResult(new Review
                {
                    Stars = addDto.Stars,
                    Comment = addDto.Comment,
                    ProductId = addDto.ProductId,
                    CustomerId = addDto.CustomerId,
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddReviewDTO.");
        }

        public override async Task<Review> MapUpdateDtoToEntity<TDto>(Review entity, TDto dto)
        {
            if (dto is UpdateReviewDTO updateDto)
            {
                if (updateDto.Stars is not null)
                    entity.Stars = updateDto.Stars ?? default;

                if (updateDto.Comment is not null)
                    entity.Comment = updateDto.Comment;


                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateReviewDTO.");
        }

        public virtual async Task<IQueryable<Review>> FilterByCustomerAsync(IQueryable<Review> reviews, string customerId)
        {
            return await Task.FromResult(reviews.Where(r => r.CustomerId == customerId));
        }

        public virtual async Task<IQueryable<Review>> FilterByStarsAsync(IQueryable<Review> reviews, int stars)
        {
            return await Task.FromResult(reviews.Where(r => r.Stars == stars));
        }

        public virtual async Task<IQueryable<Review>> FilterByProductAsync(IQueryable<Review> reviews, int productId)
        {
            return await Task.FromResult(reviews.Where(r => r.ProductId == productId));
        }
    }
}
