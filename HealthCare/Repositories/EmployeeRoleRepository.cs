using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.EmployeeRoleEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Repositories;

public class EmployeeRoleRepository(HealthCareContext context) : IEmployeeRoleRepository
{
    public async Task<IdentityUserRole<Guid>> CreateAsync(EmployeeRole employeeRole)
    {
        var entity = await context.UserRoles.AddAsync(employeeRole);
        return entity.Entity;
    }

    public void Delete(EmployeeRole employeeRole)
    {
        context.UserRoles.Remove(employeeRole);
    }

    public Task<IdentityUserRole<Guid>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}