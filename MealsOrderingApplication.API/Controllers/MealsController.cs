using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        public MealsController(IUnitOfWork unitOfWork, IAddMealValidation addMealValidation, IUpdateMealValidation updateMealValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addMealValidation = addMealValidation;
            _updateMealValidation = updateMealValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddMealValidation _addMealValidation;
        protected readonly IUpdateMealValidation _updateMealValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? name = null, int? categoryId = null, int? minPrice = null, int? maxPrice = null)
        {
            IQueryable<Meal> meals = await _unitOfWork.Meals.GetAllAsync();

            if (name is not null)
                meals = await _unitOfWork.Meals.FilterByNameAsync(meals, name);

            if (categoryId is not null)
                meals = await _unitOfWork.Meals.FilterByCategoryAsync(meals, categoryId ?? default);

            if (minPrice is not null || maxPrice is not null)
                meals = await _unitOfWork.Meals.FilterByPriceAsync(meals, minPrice, maxPrice);

            PagedResponse<MealDTODetails> response = new(
                           meals.Select(m => new MealDTODetails
                           {
                               Id = m.Id,
                               Name = m.Name,
                               Description = m.Description,
                               Price = m.Price,
                               CategoryId = m.CategoryId,
                           }),
                     _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(AddMealDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addMealValidation.AddIsValidAsync(model);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            Meal meal = await _unitOfWork.Meals.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Created(nameof(GetByIdAsync), MealDtoDetailsResponse(meal));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Meals found with this Id = {id}"
                });

            return Ok(MealDtoDetailsResponse(meal));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateMealDTO dto)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Meals found with this Id = {id}"
                });

            string message = await _updateMealValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            meal = await _unitOfWork.Meals.UpdateAsync(meal, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(MealDtoDetailsResponse(meal));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Meals found with this Id = {id}"
                });

            await _unitOfWork.Meals.DeleteAsync(meal);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<MealDTODetails> MealDtoDetailsResponse(Meal meal)
        {
            return new Response<MealDTODetails>(
                new MealDTODetails()
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    Description = meal.Description,
                    Price = meal.Price,
                    CategoryId = meal.CategoryId,
                });
        }
    }
}
