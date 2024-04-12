using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MealsOrderingApplication.Domain.DTOs.Customer;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.DTOs.Admin;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        public AdminsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IQueryable<Admin> admins = await _unitOfWork.Admins.GetAllAsync();

            return Ok(admins.Select(c => new AdminDTODetails()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Username = c.UserName
            }));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddAdminDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthanticationModel authModel = await _unitOfWork.Admins.AddAsync(model);
            if (!authModel.IsAuthenticated)
                return BadRequest(authModel.Message);

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                UserId = authModel.UserId,
                Email = authModel.Email,
                Username = authModel.UserName,
                Token = authModel.AccessToken,
            });
        }

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
                Email = admin.Email,
                Username = admin.UserName
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateAdminDTO model)
        {
            Admin? admin = await _unitOfWork.Admins.GetByIdAsync(id);
            if (admin is null)
                return NotFound(new { error = "No Admins found with this Id" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.FirstName is not null)
                admin.FirstName = model.FirstName;

            if (model.LastName is not null)
                admin.LastName = model.LastName;

            if (model.Email is not null)
                admin.Email = model.Email;

            if (model.Username is not null)
                admin.UserName = model.Username;

            if (model.Password is not null)
                admin.PasswordHash = _userManager.PasswordHasher.HashPassword(admin, model.Password);

            await _unitOfWork.Admins.UpdateAsync(admin);
            await _unitOfWork.CompleteAsync();

            return Ok(new AdminDTODetails()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Username = admin.UserName
            });
        }

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
