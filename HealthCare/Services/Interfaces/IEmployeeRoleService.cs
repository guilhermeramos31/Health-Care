using HealthCare.Models.EntityEmployeeRole;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeRoleService
{
    Task<EmployeeRole> GetByIdAsync( Guid id );
    Task<EmployeeRole> CreateAsync( EmployeeRole employeeRole);
}