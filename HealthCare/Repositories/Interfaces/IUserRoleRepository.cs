using HealthCare.Models.EntityEmployeeRole;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Repositories.Interfaces;

public interface IEmployeeRoleRepository
{
    public Task<IdentityUserRole<Guid>> GetByIdAsync( Guid id );
    public Task<IdentityUserRole<Guid>> CreateAsync( EmployeeRole employeeRole );
    public void Delete( EmployeeRole employeeRole );
}
