using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DrinkDTO;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        public DrinksController(IUnitOfWork unitOfWork, IAddDrinkValidation addDrinkValidation, IUpdateDrinkValidation updateDrinkValidation)
        {
            _unitOfWork = unitOfWork;
            _addDrinkValidation = addDrinkValidation;
            _updateDrinkValidation = updateDrinkValidation;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddDrinkValidation _addDrinkValidation;
        protected readonly IUpdateDrinkValidation _updateDrinkValidation;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Drink> drinks = await _unitOfWork.Drinks.GetAllAsync();
            return Ok(drinks.Select(d => new DrinkDTODetails()
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddDrinkDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addDrinkValidation.AddIsValidAsync(model);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            Drink drink = await _unitOfWork.Drinks.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Ok(new DrinkDTODetails()
            {
                Id = drink.Id,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                CategoryId = drink.CategoryId,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new { error = "No Drinks found with this Id" });

            return Ok(new MealDTODetails()
            {
                Id = drink.Id,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                CategoryId = drink.CategoryId,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateDrinkDTO dto)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new { error = "No Drinks found with this Id" });

            string message = await _updateDrinkValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            drink = await _unitOfWork.Drinks.UpdateAsync(drink, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new MealDTODetails()
            {
                Id = drink.Id,
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                CategoryId = drink.CategoryId,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new { error = "No Drinks found with this Id" });

            await _unitOfWork.Drinks.DeleteAsync(drink);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
