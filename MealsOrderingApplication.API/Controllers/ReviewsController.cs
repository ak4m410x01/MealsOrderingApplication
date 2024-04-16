using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(IUnitOfWork unitOfWork, IAddReviewValidation addReviewValidation, IUpdateReviewValidation updateReviewValidation) : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IAddReviewValidation _addReviewValidation = addReviewValidation;
        protected readonly IUpdateReviewValidation _updateReviewValidation = updateReviewValidation;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addReviewValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateReviewValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

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
