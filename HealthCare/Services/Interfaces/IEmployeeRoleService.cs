using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityRole.Enum;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeRoleService
{
    Task CreateAsync(Employee user, Role role);
}