﻿
namespace HealthCare.Repositories.Interfaces;

public interface IRepositoryUow
{
    IEmployeeRepository EmployeeRepository { get; }
    void Commit();
    Task CommitAsync();
    void Rollback();
    void BeginTransaction();
    Task RollbackAsync();
}
