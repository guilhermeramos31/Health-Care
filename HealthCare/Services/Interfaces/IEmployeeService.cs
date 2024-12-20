using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponse> CreateAsync( EmployeeRequest? request );
    Task<LoginResponse> LoginAsync( LoginRequest? request );
}
