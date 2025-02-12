using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Infrastructure.Managers.Interfaces;

public interface IManagerUow
{
    UserManager<Employee> UserManager { get; }
    RoleManager<Role> RoleManager { get; }
}