using HealthCare.Models.HealthSituationEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface IHealthSituationService
{
    Task<HealthSituationResponse> Create(HealthSituationRequest request);
    Task<HealthSituationResponse> Update(Guid healthSituationId, HealthSituationRequest request);
    Task Delete(Guid id);
    Task<HealthSituationResponse> GetHealthSituationById(Guid id);
    Task<HealthSituationPageResult> GetAllHealthSituations(Guid patientId, int pageSize, int pageNumber);
}