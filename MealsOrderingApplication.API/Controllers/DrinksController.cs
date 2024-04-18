using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DrinkDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        public DrinksController(IUnitOfWork unitOfWork, IAddDrinkValidation addDrinkValidation, IUpdateDrinkValidation updateDrinkValidation, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _addDrinkValidation = addDrinkValidation;
            _updateDrinkValidation = updateDrinkValidation;
            _httpContextAccessor = httpContextAccessor;
        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddDrinkValidation _addDrinkValidation;
        protected readonly IUpdateDrinkValidation _updateDrinkValidation;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Drink> drinks = await _unitOfWork.Drinks.GetAllAsync();
            return Ok(new PagedResponse<DrinkDTODetails>(
                drinks.Select(d => new DrinkDTODetails
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price,
                    CategoryId = d.CategoryId,
                }),
                _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddDrinkDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addDrinkValidation.AddIsValidAsync(model);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            Drink drink = await _unitOfWork.Drinks.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Created(nameof(GetByIdAsync), DrinkDtoDetailsResponse(drink));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Drinks found with this Id = {id}"
                });

            return Ok(DrinkDtoDetailsResponse(drink));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateDrinkDTO dto)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Drinks found with this Id = {id}"
                });

            string message = await _updateDrinkValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = message,
                });

            drink = await _unitOfWork.Drinks.UpdateAsync(drink, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(DrinkDtoDetailsResponse(drink));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"No Meals found with this Id = {id}"
                });

            await _unitOfWork.Drinks.DeleteAsync(drink);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        protected virtual Response<DrinkDTODetails> DrinkDtoDetailsResponse(Drink drink)
        {
            return new Response<DrinkDTODetails>(
                new DrinkDTODetails()
                {
                    Id = drink.Id,
                    Name = drink.Name,
                    Description = drink.Description,
                    Price = drink.Price,
                    CategoryId = drink.CategoryId,
                });
        }
    }
}
