using HealthCare.Models.EntityEmployeeRole;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class EmployeeRoleService( IEmployeeRoleRepository employeeRoleRepository ) : IEmployeeRoleService
{
    private readonly IEmployeeRoleRepository _employeeRoleRepository = employeeRoleRepository;
    public async Task<EmployeeRole> CreateAsync( EmployeeRole employeeRole )
    {
        var entity = await _employeeRoleRepository.CreateAsync( employeeRole );
        return entity;
    }
    public Task<EmployeeRole> GetByIdAsync( Guid id )
    {
        throw new NotImplementedException();
    }
}
