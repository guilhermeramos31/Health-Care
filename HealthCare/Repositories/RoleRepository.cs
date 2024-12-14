using HealthCare.Context;
using HealthCare.Models.EntityRole;
using HealthCare.Repositories.Interfaces;
using Humanizer;

namespace HealthCare.Repositories;

public class RoleRepository( HeathCareContext context ) : IRoleRepository
{
    private readonly HeathCareContext _context = context;

    public async Task<Role> CreateAsync( Role role )
    {
        var entityRole = await _context.Roles.AddAsync( role );
        return entityRole.Entity;
    }

    public async Task<Role> GetByIdAsync( Guid id )
    {
        var entityRole = await _context.Roles.FindAsync( id ) ?? throw new NoMatchFoundException( "Role not found!" );
        return entityRole;
    }

    public Task<Role> GetByName( string name )
    {
        throw new NotImplementedException();
    }
}
