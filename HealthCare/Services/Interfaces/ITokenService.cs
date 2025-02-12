using System.Security.Claims;
using HealthCare.Models.EmployeeEntity;
using Microsoft.IdentityModel.Tokens;

namespace HealthCare.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(Employee employee);
    Task<string> GenerateRefreshToken();
    Task CreateUserToken(Employee employee, string token);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken();
    ClaimsPrincipal ValidateToken(string token, TokenValidationParameters toTokenValidationParams);
}