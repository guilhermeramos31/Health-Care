using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity.Enum;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeRoleService
{
    Task CreateAsync(Employee user, Role role);
}