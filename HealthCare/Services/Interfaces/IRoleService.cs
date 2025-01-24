using HealthCare.Models.RoleEntity;
using HealthCare.Models.RoleEntity.DTO;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services.Interfaces;

public interface IRoleService
{
    Task<Role> GetByIdAsync( Guid id );
    Task<IdentityResult> CreateRole( RoleRequest roleRequest );
    Task DeleteById( Guid id );
}
