using AutoMapper;
using HealthCare.Models.NutritionalAssessmentEntity;
using HealthCare.Models.NutritionalAssessmentEntity.Dto;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class NutritionalAssessmentService(IMapper mapper, IRepositoryUow repositoryUow, IPatientService patientService)
    : INutritionalAssessmentService
{
    public async Task<NutritionalAssessmentResponse> Create(Guid patientId, NutritionalAssessmentRequest request)
    {
        var patient = await patientService.GetPatientEntity(patientId);
        var nutritionalAssessment = mapper.Map<NutritionalAssessment>(request);
        nutritionalAssessment.Patient = patient;
        nutritionalAssessment.PatientId = patient.Id;

        await repositoryUow.NutritionalAssessmentRepository.Create(nutritionalAssessment);
        await repositoryUow.CommitAsync();

        return mapper.Map<NutritionalAssessmentResponse>(nutritionalAssessment);
    }

    public async Task<NutritionalAssessmentResponse> Update(Guid nutritionalAssessmentId,
        NutritionalAssessmentRequest request)
    {
        var nutritionalAssessment = await GetNutritionalAssessmentEntity(nutritionalAssessmentId);
        nutritionalAssessment = mapper.Map(request, nutritionalAssessment);

        repositoryUow.NutritionalAssessmentRepository.Update(nutritionalAssessment);
        await repositoryUow.CommitAsync();

        return mapper.Map<NutritionalAssessmentResponse>(nutritionalAssessment);
    }

    public async Task Delete(Guid nutritionalAssessmentId)
    {
        var nutritionalAssessment = await GetNutritionalAssessmentEntity(nutritionalAssessmentId);
        repositoryUow.NutritionalAssessmentRepository.Delete(nutritionalAssessment);
        await repositoryUow.CommitAsync();
    }

    public async Task<NutritionalAssessmentResponse> GetNutritionalAssessmentById(Guid nutritionalAssessmentId)
    {
        return mapper.Map<NutritionalAssessmentResponse>(await GetNutritionalAssessmentEntity(nutritionalAssessmentId));
    }

    public async Task<NutritionalAssessmentPageResponse> GetAllNutritionalAssessments(Guid patientId, int pageSize,
        int pageNumber)
    {
        var nutritionalAssessment =
            await repositoryUow.NutritionalAssessmentRepository.GetAll(patientId, pageSize, pageNumber);

        return new()
        {
            NutritionalAssessmentResponses = mapper.Map<List<NutritionalAssessmentResponse>>(nutritionalAssessment),
            TotalCount = nutritionalAssessment.Count
        };
    }

    private async Task<NutritionalAssessment> GetNutritionalAssessmentEntity(Guid nutritionalAssessmentId)
    {
        return await repositoryUow.NutritionalAssessmentRepository.GetById(nutritionalAssessmentId) ??
               throw new BadHttpRequestException("NutritionalAssessment not found");
    }
}