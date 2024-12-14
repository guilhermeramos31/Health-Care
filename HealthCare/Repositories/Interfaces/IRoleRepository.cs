using HealthCare.Models.EntityRole;

namespace HealthCare.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync( Guid id );
    Task<Role> CreateAsync( Role role );
    Task<Role> GetByName( string name );
}
