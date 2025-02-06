using HealthCare.Infrastructure.Data.Context;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare.Repositories;

public class RepositoryUow(HealthCareContext context) : IRepositoryUow
{
    private IDbContextTransaction? _transaction = null;
    private IEmployeeRepository? _employeeRepository = null;

    public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(context);

    public void Dispose()
    {
        _transaction?.Dispose();
    }

    public async Task DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
    }

    public void Commit()
    {
        context.SaveChanges();
        _transaction?.Commit();
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
    }

    public void BeginTransaction()
    {
        _transaction = context.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }
}