using MealsOrderingApplication.Domain.IdentityEntities;
using System.IdentityModel.Tokens.Jwt;

namespace MealsOrderingApplication.Services.IServices
{
    internal interface IJWTToken
    {
        Task<JwtSecurityToken> GenerateAccessTokenAsync(ApplicationUser user);
    }
}
