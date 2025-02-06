using System.Security.Claims;
using HealthCare.Models.EmployeeEntity;

namespace HealthCare.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(Employee employee);
    Task<string> GenerateRefreshToken();
    Task CreateUserToken(Employee employee, string token);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken();
}