using HealthCare.Models.EmployeeEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> GetByIdAsync(string id);
    Task<Employee> GetByEmailAsync(string email);
    Task<Employee> GetByUserNameAsync(string userName);
    Task<Employee> GetByUserNameOrEmailAsync(string userNameOrEmail);
    Task<Employee> CreateAsync(Employee employee);
}