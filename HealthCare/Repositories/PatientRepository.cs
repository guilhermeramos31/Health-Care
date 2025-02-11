using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.ProfessionalPatientEntity;
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

    public async Task<Patient?> GetPatient(string id)
    {
        var patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Id.Equals(id));
        return patient;
    }

    public void Delete(Patient patient)
    {
        dbContext.Patients.Remove(patient);
    }
}