using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.PatientEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories;

public class PatientRepository(HealthCareContext dbContext) : IPatientRepository
{
    public async Task<Patient> Create(Patient newPatient)
    {
        var patient = await dbContext.Patients.AddAsync(newPatient);
        return patient.Entity;
    }

    public Patient Update(Patient updatePatient)
    {
        var updatedPatient = dbContext.Patients.Update(updatePatient);
        return updatedPatient.Entity;
    }

    public async Task<Patient?> FindByCpf(string cpf)
    {
        var patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Cpf.Equals(cpf));
        return patient;
    }

    public async Task<Patient?> GetPatient(Guid id, bool asNoTracking = false)
    {
        var patient = dbContext.Patients.AsQueryable();
        if (asNoTracking)
        {
            patient = patient.AsNoTracking();
        }

        return await patient.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Delete(Patient patient)
    {
        dbContext.Patients.Remove(patient);
    }

    public Task<List<Patient>> GetPatients(Guid userId, int? pageNumber, int? pageSize)
    {
        var patients = dbContext.Patients.Where(p => p.ProfessionalPatients.Any(pp => pp.EmployeeId == userId));
        if (pageSize.HasValue && pageNumber.HasValue && pageSize.Value > 0 && pageNumber > 0)
        {
            patients = patients.Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }


        patients = patients.Include(patient => patient.Address)
            .OrderByDescending(patient => patient.Name);

        return patients.ToListAsync();
    }
}