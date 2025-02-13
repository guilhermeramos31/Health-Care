using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.NutritionalAssessmentEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories;

public class NutritionalAssessmentRepository(HealthCareContext dbContext) : INutritionalAssessmentRepository
{
    public async Task<NutritionalAssessment> Create(NutritionalAssessment newNutritionalAssessment)
    {
        var nutritionalAssessment = await dbContext.NutritionalAssessments.AddAsync(newNutritionalAssessment);
        return nutritionalAssessment.Entity;
    }

    public NutritionalAssessment Update(NutritionalAssessment updatedNutritionalAssessment)
    {
        var nutritionalAssessment = dbContext.NutritionalAssessments.Update(updatedNutritionalAssessment);
        return nutritionalAssessment.Entity;
    }

    public void Delete(NutritionalAssessment deletedNutritionalAssessment)
    {
        dbContext.Remove(deletedNutritionalAssessment);
    }

    public async Task<NutritionalAssessment?> GetById(Guid id)
    {
        return await dbContext.NutritionalAssessments.FindAsync(id);
    }

    public Task<List<NutritionalAssessment>> GetAll(Guid patientId, int? pageSize, int? pageNumber)
    {
        var nutritionalAssessment = dbContext.NutritionalAssessments.Where(hs => hs.PatientId == patientId);
        if (pageSize.HasValue && pageNumber.HasValue && pageSize.Value > 0 && pageNumber > 0)
        {
            nutritionalAssessment = nutritionalAssessment.Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        nutritionalAssessment = nutritionalAssessment.Include(hs => hs.Patient)
            .OrderByDescending(hs => hs.CreateAt);

        return nutritionalAssessment.ToListAsync();
    }
}