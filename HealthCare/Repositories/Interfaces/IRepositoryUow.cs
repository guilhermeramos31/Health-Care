﻿namespace HealthCare.Repositories.Interfaces;

public interface IRepositoryUow
{
    IEmployeeRepository EmployeeRepository { get; }
    IPatientRepository PatientRepository { get; }
    IAddressRepository AddressRepository { get; }
    IHealthSituationsRepository HealthSituationsRepository { get; }

    IProfessionalPatientRepository ProfessionalPatientRepository { get; }

    void Commit();
    Task CommitAsync();
    void Rollback();
    void BeginTransaction();
    Task RollbackAsync();
}