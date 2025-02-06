﻿using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Repositories.Interfaces;

namespace HealthCare.Repositories;

public class EmployeeRepository(HealthCareContext context) : IEmployeeRepository
{
    private readonly HealthCareContext _context = context;

    public async Task<Employee> CreateAsync(Employee employee)
    {
        var entityEmployee = await _context.Users.AddAsync(employee);
        return entityEmployee.Entity;
    }

    public Task<Employee> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
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