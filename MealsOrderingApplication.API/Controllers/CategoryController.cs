using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Category> categories = await _unitOfWork.Categories.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCategoryDTO model)
        {
            Category category = new Category()
            {
                Name = model.Name,
                Description = model.Description,
            };
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return BadRequest(new { error = "No Categories found with this Id" });
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryDTO model)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return BadRequest(new { error = "No Categories found with this Id" });

            if (model.Name is not null)
                category.Name = model.Name;

            if (model.Description is not null)
                category.Description = model.Description;

            await _unitOfWork.CompleteAsync();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return BadRequest(new { error = "No Categories found with this Id" });

            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

    }
}
