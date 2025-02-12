using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity.Enum;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class EmployeeRoleService(IManagerUow managerUow) : IEmployeeRoleService
{
    public async Task CreateAsync(Employee user, Role role)
    {
        var result = await managerUow.UserManager.AddToRoleAsync(user, role.ToString());
        if (!result.Succeeded)
        {
            throw new BadHttpRequestException("Failed to add role to user");
        }
    }
}