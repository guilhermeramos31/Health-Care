using HealthCare.Infrastructure.Data.Context;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare.Repositories;

public class RepositoryUow(HealthCareContext dbContext) : IRepositoryUow
{
    private IDbContextTransaction? _transaction;
    private IEmployeeRepository? _employeeRepository;
    private IAddressRepository? _addressRepository;
    private IPatientRepository? _patientRepository;
    private IProfessionalPatientRepository? _professionalPatient;
    private IHealthSituationsRepository? _healthSituationsRepository;

    public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(dbContext);
    public IAddressRepository AddressRepository => _addressRepository ??= new AddressRepository(dbContext);
    public IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(dbContext);

    public IHealthSituationsRepository HealthSituationsRepository =>
        _healthSituationsRepository ??= new HealthSituationsRepository(dbContext);

    public IProfessionalPatientRepository ProfessionalPatientRepository =>
        _professionalPatient ??= new ProfessionalPatientRepository(dbContext);

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
        dbContext.SaveChanges();
        _transaction?.Commit();
    }

    public async Task CommitAsync()
    {
        await dbContext.SaveChangesAsync();
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
        _transaction = dbContext.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await dbContext.Database.BeginTransactionAsync();
    }
}