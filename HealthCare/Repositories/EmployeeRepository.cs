using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Repositories.Interfaces;

namespace HealthCare.Repositories;

public class EmployeeRepository(HealthCareContext dbContext) : IEmployeeRepository
{
    public async Task<Employee> CreateAsync(Employee employee)
    {
        var entityEmployee = await dbContext.Users.AddAsync(employee);
        return entityEmployee.Entity;
    }

    public Task<Employee> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        return await dbContext.FindAsync<Employee>(id);
    }

    public Task<Employee> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByUserNameOrEmailAsync(string userNameOrEmail)
    {
        throw new NotImplementedException();
    }
}