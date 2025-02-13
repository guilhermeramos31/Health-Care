using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.MedicationEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories;

public class MedicationRepository(HealthCareContext dbContext) : IMedicationRepository
{
    public async Task<Medication> Create(Medication newMedication)
    {
        var medication = await dbContext.Medications.AddAsync(newMedication);
        return medication.Entity;
    }

    public Medication Update(Medication updateMedication)
    {
        var medication = dbContext.Medications.Update(updateMedication);
        return medication.Entity;
    }

    public void Delete(Medication medication)
    {
        dbContext.Medications.Remove(medication);
    }

    public async Task<Medication?> GetMedicationById(Guid medicationId)
    {
        return await dbContext.Medications.FindAsync(medicationId);
    }

    public Task<List<Medication>> GetAllMedications(Guid patientId, int? pageSize, int? pageNumber)
    {
        var query = dbContext.Medications.Where(x => x.PatientId == patientId);
        if (pageSize.HasValue && pageNumber.HasValue && pageSize.Value > 0 && pageNumber > 0)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        query = query.Include(hs => hs.Patient)
            .OrderByDescending(x => x.CreateAt);

        return query.ToListAsync();
    }
}