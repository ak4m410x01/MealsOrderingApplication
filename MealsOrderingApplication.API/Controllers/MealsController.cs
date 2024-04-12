using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        public MealsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

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
        public async Task<IActionResult> AddAsync(AddMealDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if ((await _unitOfWork.Categories.GetByIdAsync(model.CategoryId)) is null)
                return BadRequest(new { CategoryId = "Invalid Category Id" });

            Meal meal = new Meal()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            await _unitOfWork.Meals.AddAsync(meal);
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
        public async Task<IActionResult> UpdateAsync(int id, UpdateMealDTO model)
        {
            Meal? meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal is null)
                return NotFound(new { error = "No meals found with this Id" });


            if (model.Name is not null)
                meal.Name = model.Name;

            if (model.Description is not null)
                meal.Description = model.Description;

            if (model.Price is not null)
                meal.Price = (double)model.Price;

            if (model.CategoryId is not null)
            {
                int categoryId = (int)model.CategoryId;
                if ((await _unitOfWork.Categories.GetByIdAsync(categoryId)) is null)
                    return BadRequest(new { error = "Invalid Category Id" });
                meal.CategoryId = categoryId;
            }

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
