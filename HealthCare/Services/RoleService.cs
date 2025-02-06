using AutoMapper;
using HealthCare.Models.RoleEntity;
using HealthCare.Models.RoleEntity.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services;

public class RoleService(RoleManager<Role> roleManager, IMapper mapper) : IRoleService
{
    public async Task<IdentityResult> CreateRole(RoleRequest roleRequest)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleRequest.Name);
        return !roleExist
            ? await roleManager.CreateAsync(mapper.Map<Role>(roleRequest))
            : IdentityResult.Failed(new IdentityError { Code = "RoleExist", Description = "Role already exist" });
    }

    public async Task DeleteById(Guid id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role != null)
        {
            await roleManager.DeleteAsync(role);
        }
    }

    public async Task<Role> GetByIdAsync(Guid id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role == null) throw new Exception("Role not found!");
        return role;
    }
}