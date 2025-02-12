using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.ProfessionalPatientEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories;

public class ProfessionalPatientRepository(HealthCareContext dbContext) : IProfessionalPatientRepository
{
    public async Task<ProfessionalPatient> Create(ProfessionalPatient professionalPatient)
    {
        var newProfessionalPatient = await dbContext.ProfessionalPatients.AddAsync(professionalPatient);
        return newProfessionalPatient.Entity;
    }

    public ProfessionalPatient Update(ProfessionalPatient professionalPatient)
    {
        var updatedProfessionalPatient = dbContext.ProfessionalPatients.Update(professionalPatient);
        return updatedProfessionalPatient.Entity;
    }

    public void Delete(ProfessionalPatient professionalPatient)
    {
        dbContext.ProfessionalPatients.Remove(professionalPatient);
    }

    public async Task<ProfessionalPatient?> GetByPatientId(Guid patientId)
    {
        return await dbContext.ProfessionalPatients.FirstOrDefaultAsync(pp => pp.PatientId == patientId);
    }
}