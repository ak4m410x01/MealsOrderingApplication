using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Review> reviews = await _unitOfWork.Reviews.GetAllAsync();

            return Ok(reviews.Select(c =>
                    new ReviewDTODetails()
                    {
                        Id = c.Id,
                        Stars = c.Stars,
                        Comment = c.Comment,
                        ProductId = c.ProductId,
                        CustomerId = c.CustomerId,
                    }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddReviewDTO dto)
        {
            if ((await _unitOfWork.Products.GetByIdAsync(dto.ProductId)) is null)
                return BadRequest(new { error = "Invalid ProductId" });

            if ((await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId)) is null)
                return BadRequest(new { error = "Invalid CustomerId" });

            Review review = await _unitOfWork.Reviews.AddAsync(dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new ReviewDTODetails()
            {
                Id = review.Id,
                Stars = review.Stars,
                Comment = review.Comment,
                ProductId = review.ProductId,
                CustomerId = review.CustomerId,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new { error = "No Reviews found with this Id" });

            return Ok(new ReviewDTODetails()
            {
                Id = review.Id,
                Stars = review.Stars,
                Comment = review.Comment,
                ProductId = review.ProductId,
                CustomerId = review.CustomerId,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateReviewDTO dto)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new { error = "No Reviews found with this Id" });

            if (dto.ProductId is not null && (await _unitOfWork.Products.GetByIdAsync(dto.ProductId)) is null)
                return BadRequest(new { error = "Invalid ProductId" });

            await _unitOfWork.Reviews.UpdateAsync(review, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new ReviewDTODetails()
            {
                Id = review.Id,
                Stars = review.Stars,
                Comment = review.Comment,
                ProductId = review.ProductId,
                CustomerId = review.CustomerId,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new { error = "No Reviews found with this Id" });

            await _unitOfWork.Reviews.DeleteAsync(review);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
