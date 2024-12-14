using HealthCare.Context;
using HealthCare.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Repositories;

public class EmployeeRoleRepository( HeathCareContext context ) : IEmployeeRoleRepository
{
    private readonly HeathCareContext _context = context;

    public async Task<IdentityUserRole<Guid>> CreateAsync( IdentityUserRole<Guid> employeeRole )
    {
        var entity = await _context.UserRoles.AddAsync( employeeRole );
        return entity.Entity;
    }

    public void Delete( IdentityUserRole<Guid> employeeRole )
    {
        _context.UserRoles.Remove( employeeRole );
    }

    public Task<IdentityUserRole<Guid>> GetByIdAsync( Guid id )
    {
        throw new NotImplementedException();
    }
}
