using AutoMapper;
using HealthCare.Models.EntityRole;
using HealthCare.Models.EntityRole.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services;

public class RoleService( RoleManager<Role> roleManager, IMapper mapper ) : IRoleService
{
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;

    public async Task<IdentityResult> CreateRole( RoleRequest roleRequest )
    {
        var roleExist = await _roleManager.RoleExistsAsync( roleRequest.Name );
        return !roleExist ? await _roleManager.CreateAsync( _mapper.Map<Role>( roleRequest ) )
            : IdentityResult.Failed( new IdentityError { Code = "RoleExist", Description = "Role already exist" } );
    }

    public async Task DeleteById( Guid id )
    {
        var role = await _roleManager.FindByIdAsync( id.ToString() );
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
    }

    public async Task<Role> GetByIdAsync( Guid id )
    {
        var role = await _roleManager.FindByIdAsync( id.ToString() );
        if (role == null) throw new Exception( "Role not found!" );
        return role;
    }
}
