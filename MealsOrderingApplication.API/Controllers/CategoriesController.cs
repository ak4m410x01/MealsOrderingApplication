using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;

        }

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IHttpContextAccessor _httpContextAccessor;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Category> categories = await _unitOfWork.Categories.GetAllAsync();

            return Ok(new PagedResponse<CategoryDTODetails>(
                categories.Select(c => new CategoryDTODetails
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                }),
                _httpContextAccessor.HttpContext!.Request, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddCategoryDTO dto)
        {
            Category category = await _unitOfWork.Categories.AddAsync(dto);
            await _unitOfWork.CompleteAsync();

            return Created(nameof(GetByIdAsync), CategoryDtoDetailsResponse(category));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"Use Valid Id.",
                    Errors = [$"No Categories found with Id = {id}"]
                });

            return Ok(CategoryDtoDetailsResponse(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"Use Valid Id.",
                    Errors = [$"No Categories found with Id = {id}"]
                });

            await _unitOfWork.Categories.UpdateAsync(category, dto);

            await _unitOfWork.CompleteAsync();

            return Ok(CategoryDtoDetailsResponse(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return NotFound(new Response<object>()
                {
                    Succeeded = false,
                    Message = $"Use Valid Id.",
                    Errors = [$"No Categories found with Id = {id}"]
                });

            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        protected Response<CategoryDTODetails> CategoryDtoDetailsResponse(Category category)
        {
            return new Response<CategoryDTODetails>(
                new CategoryDTODetails()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
        }
    }
}
