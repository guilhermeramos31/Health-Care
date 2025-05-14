using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Models.Query;
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

    public Task<List<Patient>> FindByCpfOrCns(string search)
    {
        var patient = dbContext.Patients.Where(p => p.Cpf.Contains(search) || p.Cns.Contains(search) || p.Name.ToLower().Contains(search.ToLower()));
        return patient.ToListAsync();
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

    public async Task<PatientPage> GetPatients(Guid userId, QueryParameters parameters)
    {
        var patients = dbContext.Patients.Where(p => p.ProfessionalPatients.Any(pp => pp.EmployeeId == userId));
        if (!string.IsNullOrEmpty(parameters.Search)) patients = patients.Where(p => p.Name.ToLower().Contains(parameters.Search.ToLower()));
        var patientsCount = patients.Count();
        patients = parameters.Filter == Filter.DESCENDING? patients.OrderByDescending(patient => patient.Name) 
            : patients.OrderBy(patient => patient.Name);
        patients = patients.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);
        patients = patients.Include(patient => patient.Address)
            .OrderByDescending(patient => patient.AdmissionDate);
        
        return new ()
        {
            Patients = await patients.ToListAsync(),
            TotalCount = patientsCount
        };
    }
}

