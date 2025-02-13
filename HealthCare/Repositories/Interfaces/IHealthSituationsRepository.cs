using HealthCare.Models.HealthSituationEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IHealthSituationsRepository
{
    Task<HealthSituation> Create(HealthSituation healthSituation);
    HealthSituation Update(HealthSituation healthSituation);
    void Delete(HealthSituation healthSituation);
    Task<HealthSituation?> GetHealthSituationById(int healthSituationId);
    Task<List<HealthSituation>> GetAllHealthSituations(Guid patientId, int? pageSize, int? pageNumber);
}