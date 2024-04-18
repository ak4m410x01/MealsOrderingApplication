using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAddAdminValidation _addAdminValidation;
        protected readonly IUpdateAdminValidation _updateAdminValidation;

        public AdminsController(IUnitOfWork unitOfWork, IUpdateAdminValidation updateAdminValidation, IAddAdminValidation addAdminValidation)
        {
            _unitOfWork = unitOfWork;
            _addAdminValidation = addAdminValidation;
            _updateAdminValidation = updateAdminValidation;
        }

        // Retrieve All Admins
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Admin> admins = await _unitOfWork.Admins.GetAllAsync();

            return Ok(admins.Select(c => new AdminDTODetails()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email!,
                Username = c.UserName!
            }));
        }


        // Add new Admin
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddAdminDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _addAdminValidation.AddIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            AuthanticationModel authModel = await _unitOfWork.Admins.CreateAsync(dto);
            if (!authModel.IsAuthenticated)
                return BadRequest(new { error = authModel.Message });

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                authModel.UserId,
                authModel.Email,
                Username = authModel.UserName,
            });
        }

        // Retrieve Admin By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            return Ok(new AdminDTODetails()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email!,
                Username = admin.UserName!
            });
        }


        // Update exists Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateAdminDTO dto)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string message = await _updateAdminValidation.UpdateIsValidAsync(dto);
            if (!string.IsNullOrEmpty(message))
                return BadRequest(new { error = message });

            await _unitOfWork.Admins.UpdateAsync(admin, dto);
            await _unitOfWork.CompleteAsync();

            return Ok(new AdminDTODetails()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email!,
                Username = admin.UserName!
            });
        }


        // Delete Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            await _unitOfWork.Admins.DeleteAsync(admin);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
