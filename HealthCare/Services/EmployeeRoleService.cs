using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity.Enum;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services;

public class EmployeeRoleService(UserManager<Employee> userManager) : IEmployeeRoleService
{
    public async Task CreateAsync(Employee user, Role role)
    {
        var result = await userManager.AddToRoleAsync(user, role.ToString());
        if (!result.Succeeded)
        {
            throw new BadHttpRequestException("Failed to add role to user");
        }
    }
}