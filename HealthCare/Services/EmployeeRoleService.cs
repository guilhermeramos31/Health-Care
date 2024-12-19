using HealthCare.Models.EntityEmployee;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Role = HealthCare.Models.EntityRole.Enum.Role;

namespace HealthCare.Services;

public class EmployeeRoleService( UserManager<Employee> userManager ) : IEmployeeRoleService
{
    private readonly UserManager<Employee> _userManager = userManager;

    public async Task CreateAsync( Employee user, Role role )
    {
        var result = await _userManager.AddToRoleAsync( user, role.ToString() );
        if (!result.Succeeded)
        {
            throw new BadHttpRequestException( "Failed to add role to user" );
        }
    }
}
