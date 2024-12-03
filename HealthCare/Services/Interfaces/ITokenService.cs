using HealthCare.Models.EntityEmployee;

namespace HealthCare.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken( Employee employee );
    string GenerateRefreshToken();
}
