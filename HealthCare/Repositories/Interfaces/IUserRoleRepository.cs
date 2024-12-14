using Microsoft.AspNetCore.Identity;

namespace HealthCare.Repositories.Interfaces;

public interface IEmployeeRoleRepository
{
    public Task<IdentityUserRole<Guid>> GetByIdAsync( Guid id );
    public Task<IdentityUserRole<Guid>> CreateAsync( IdentityUserRole<Guid> employeeRole );
    public void Delete( IdentityUserRole<Guid> employeeRole );
}
