using HealthCare.Context;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare.Repositories;

public class RepositoryUow( HeathCareContext context ) : IRepositoryUow
{
    private IDbContextTransaction? _transaction = null;
    private readonly HeathCareContext _context = context;

    private IEmployeeRepository? _employeeRepository = null;
    private IRoleRepository? _roleRepository = null;
    private IEmployeeRoleRepository? _userRoleRepository = null;

    public IEmployeeRepository EmployeeRepository
    {
        get
        {
            _employeeRepository ??= new EmployeeRepository( _context );
            return _employeeRepository;
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            _roleRepository ??= new RoleRepository( _context );
            return _roleRepository;
        }
    }

    public IEmployeeRoleRepository UserRoleRepository
    {
        get
        {
            _userRoleRepository = new EmployeeRoleRepository( _context );
            return _userRoleRepository;
        }
    }

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
        _context.SaveChanges();
        _transaction?.Commit();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
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
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }
}
