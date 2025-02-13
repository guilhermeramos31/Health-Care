using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.HealthSituationEntity;
using HealthCare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories;

public class HealthSituationsRepository(HealthCareContext dbContext) : IHealthSituationsRepository
{
    public async Task<HealthSituation> Create(HealthSituation healthSituation)
    {
        var newHealthSituation = await dbContext.HealthSituation.AddAsync(healthSituation);
        return newHealthSituation.Entity;
    }

    public HealthSituation Update(HealthSituation healthSituation)
    {
        var updateHealthSituation = dbContext.HealthSituation.Update(healthSituation);
        return updateHealthSituation.Entity;
    }

    public void Delete(HealthSituation healthSituation)
    {
        dbContext.HealthSituation.Remove(healthSituation);
    }

    public async Task<HealthSituation?> GetHealthSituationById(Guid healthSituationId)
    {
        return await dbContext.HealthSituation.FindAsync(healthSituationId);
    }

    public Task<List<HealthSituation>> GetAllHealthSituations(Guid patientId, int? pageSize, int? pageNumber)
    {
        var healthSituation = dbContext.HealthSituation.Where(hs => hs.PatientId == patientId);
        if (pageSize.HasValue && pageNumber.HasValue && pageSize.Value > 0 && pageNumber > 0)
        {
            healthSituation = healthSituation.Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        healthSituation = healthSituation.Include(hs => hs.Patient)
            .OrderByDescending(hs => hs.CreateAt);

        return healthSituation.ToListAsync();
    }
}