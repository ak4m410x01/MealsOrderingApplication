using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MealsOrderingApplication.Services.IServices
{
    public interface IAuthenticationService
    {
        public Task<AuthanticationModel> RegisterAsync(RegisterDTO model);
        public Task<AuthanticationModel> LoginAsync(LoginDTO model);
        public Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);
    }
}
