using AutoMapper;
using HealthCare.Models.HealthSituationEntity;
using HealthCare.Models.HealthSituationEntity.Dto;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class HealthSituationService(IMapper mapper, IRepositoryUow repositoryUow, IPatientService patientService)
    : IHealthSituationService
{
    public async Task<HealthSituationResponse> Create(HealthSituationRequest request)
    {
        var newHealthSituation = mapper.Map<HealthSituation>(request);
        newHealthSituation = await repositoryUow.HealthSituationsRepository.Create(newHealthSituation);
        await repositoryUow.CommitAsync();

        return mapper.Map<HealthSituationResponse>(newHealthSituation);
    }

    public async Task<HealthSituationResponse> Update(Guid healthSituationId, HealthSituationRequest request)
    {
        var healthSituation =
            await repositoryUow.HealthSituationsRepository.GetHealthSituationById(healthSituationId) ??
            throw new BadHttpRequestException("Health situation not found");
        healthSituation = mapper.Map(request, healthSituation);

        repositoryUow.HealthSituationsRepository.Update(healthSituation);
        await repositoryUow.CommitAsync();

        return mapper.Map<HealthSituationResponse>(healthSituation);
    }

    public async Task Delete(Guid id)
    {
        var healthSituation = await repositoryUow.HealthSituationsRepository.GetHealthSituationById(id) ??
                              throw new BadHttpRequestException("HealthSituation not found");
        repositoryUow.HealthSituationsRepository.Delete(healthSituation);
        await repositoryUow.CommitAsync();
    }

    public async Task<HealthSituationResponse> GetHealthSituationById(Guid id)
    {
        var healthSituation = await repositoryUow.HealthSituationsRepository.GetHealthSituationById(id) ??
                              throw new NullReferenceException("HealthSituation not found");

        return mapper.Map<HealthSituationResponse>(healthSituation);
    }

    public async Task<HealthSituationPageResult> GetAllHealthSituations(Guid patientId, int pageSize, int pageNumber)
    {
        var healthSituations =
            await repositoryUow.HealthSituationsRepository.GetAllHealthSituations(patientId, pageSize, pageNumber);

        return new()
        {
            HealthSituations = mapper.Map<List<HealthSituationResponse>>(healthSituations),
            TotalCount = healthSituations.Count
        };
    }
}