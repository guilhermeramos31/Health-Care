using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Models.EntityRole.Enum;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponse> CreateAsync( EmployeeRequest? request );
}
