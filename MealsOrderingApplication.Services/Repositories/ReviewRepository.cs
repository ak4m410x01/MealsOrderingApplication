using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
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
    }
}
