using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        public MealsController(IUnitOfWork unitOfWork, IAddMealValidation addMealValidation, IUpdateMealValidation updateMealValidation)
        {
            _unitOfWork = unitOfWork;
            _addMealValidation = addMealValidation;
            _updateMealValidation = updateMealValidation;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddMealValidation _addMealValidation;
        protected readonly IUpdateMealValidation _updateMealValidation;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Meal> meals = await _unitOfWork.Meals.GetAllAsync();
            return Ok(meals.Select(m => new MealDTODetails()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                CategoryId = m.CategoryId,
            }));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(AddMealDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addMealValidation.AddIsValidAsync(model);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            Meal meal = await _unitOfWork.Meals.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Ok(new MealDTODetails()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price,
                CategoryId = meal.CategoryId,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new { error = "No Meals found with this Id" });

            return Ok(new MealDTODetails()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price,
                CategoryId = meal.CategoryId,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateMealDTO dto)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new { error = "No meals found with this Id" });

            string message = await _updateMealValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            meal = await _unitOfWork.Meals.UpdateAsync(meal, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new MealDTODetails()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price,
                CategoryId = meal.CategoryId,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new { error = "No Meals found with this Id" });

            await _unitOfWork.Meals.DeleteAsync(meal);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
