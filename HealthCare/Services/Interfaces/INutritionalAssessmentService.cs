using HealthCare.Models.NutritionalAssessmentEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface INutritionalAssessmentService
{
    Task<NutritionalAssessmentResponse> Create(Guid patientId, NutritionalAssessmentRequest request);
    Task<NutritionalAssessmentResponse> Update(Guid nutritionalAssessmentId, NutritionalAssessmentRequest request);
    Task Delete(Guid nutritionalAssessmentId);
    Task<NutritionalAssessmentResponse> GetNutritionalAssessmentById(Guid nutritionalAssessmentId);
    Task<NutritionalAssessmentPageResponse> GetAllNutritionalAssessments(Guid patientId, int pageSize, int pageNumber);
}