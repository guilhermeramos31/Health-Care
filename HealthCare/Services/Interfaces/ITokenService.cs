using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken( EmployeeRequestDTO employee );
    string GenerateRefreshToken();
}
