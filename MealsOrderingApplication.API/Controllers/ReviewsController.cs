using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.DTOs.ReviewDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        public ReviewsController(IUnitOfWork unitOfWork, IAddReviewValidation addReviewValidation, IUpdateReviewValidation updateReviewValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addReviewValidation = addReviewValidation;
            _updateReviewValidation = updateReviewValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddReviewValidation _addReviewValidation;
        protected readonly IUpdateReviewValidation _updateReviewValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Review> reviews = await _unitOfWork.Reviews.GetAllAsync();
            return Ok(new PagedResponse<ReviewDTODetails>(
                reviews.Select(r => new ReviewDTODetails
                {
                    Id = r.Id,
                    Stars = r.Stars,
                    Comment = r.Comment,
                    ProductId = r.ProductId,
                    CustomerId = r.CustomerId,
                }),
                _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addReviewValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            Review review = await _unitOfWork.Reviews.AddAsync(dto);
            await _unitOfWork.CompleteAsync();

            return Created(nameof(GetByIdAsync), ReviewDtoDetailsResponse(review));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Reviews found with this Id = {id}"
                });

            return Ok(ReviewDtoDetailsResponse(review));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateReviewDTO dto)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Reviews found with this Id = {id}"
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateReviewValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            await _unitOfWork.Reviews.UpdateAsync(review, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(ReviewDtoDetailsResponse(review));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Review? review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Reviews found with this Id = {id}"
                });

            await _unitOfWork.Reviews.DeleteAsync(review);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<ReviewDTODetails> ReviewDtoDetailsResponse(Review review)
        {
            return new Response<ReviewDTODetails>(
                new ReviewDTODetails()
                {
                    Id = review.Id,
                    Stars = review.Stars,
                    Comment = review.Comment,
                    ProductId = review.ProductId,
                    CustomerId = review.CustomerId,
                });
        }
    }
}
