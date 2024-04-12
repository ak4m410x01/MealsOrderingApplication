﻿using MealsOrderingApplication.Domain;
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
        public DrinksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

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

            if ((await _unitOfWork.Categories.GetByIdAsync(model.CategoryId)) is null)
                return BadRequest(new { CategoryId = "Invalid Category Id" });

            Drink drink = new Drink()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            await _unitOfWork.Drinks.AddAsync(drink);
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
        public async Task<IActionResult> UpdateAsync(int id, UpdateDrinkDTO model)
        {
            Drink? drink = await _unitOfWork.Drinks.GetByIdAsync(id);
            if (drink is null)
                return NotFound(new { error = "No Drinks found with this Id" });


            if (model.Name is not null)
                drink.Name = model.Name;

            if (model.Description is not null)
                drink.Description = model.Description;

            if (model.Price is not null)
                drink.Price = (double)model.Price;

            if (model.CategoryId is not null)
            {
                int categoryId = (int)model.CategoryId;
                if ((await _unitOfWork.Categories.GetByIdAsync(categoryId)) is null)
                    return BadRequest(new { error = "Invalid Category Id" });
                drink.CategoryId = categoryId;
            }

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