﻿using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.IServices;
using MealsOrderingApplication.Services.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealsOrderingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthenticationService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public readonly IUnitOfWork _unitOfWork;
        public readonly IAuthenticationService _authService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthanticationModel authModel = await _authService.RegisterAsync(model);
            if (!authModel.IsAuthenticated)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = authModel.Message,
                });

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                Username = authModel.UserName,
                Token = authModel.AccessToken,
                Expiration = authModel.AccessTokenExpiration,
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            AuthanticationModel authModel = await _authService.LoginAsync(model);
            if (!authModel.IsAuthenticated)
                return BadRequest(new Response<object>()
                {
                    Succeeded = false,
                    Message = authModel.Message,
                });

            return Ok(new
            {
                Token = authModel.AccessToken,
                Expiration = authModel.AccessTokenExpiration,
            });
        }
    }
}
