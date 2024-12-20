using HealthCare.Models.EntityEmployee;

namespace HealthCare.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken( Employee employee );
    Task<string> GenerateRefreshToken();
    Task CreateUserToken( Employee employee, string token );
}
