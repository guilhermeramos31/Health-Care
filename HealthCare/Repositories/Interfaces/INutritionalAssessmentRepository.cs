using HealthCare.Models.NutritionalAssessmentEntity;

namespace HealthCare.Repositories.Interfaces;

public interface INutritionalAssessmentRepository
{
    Task<NutritionalAssessment> Create(NutritionalAssessment newNutritionalAssessment);
    NutritionalAssessment Update(NutritionalAssessment updatedNutritionalAssessment);
    void Delete(NutritionalAssessment deletedNutritionalAssessment);
    Task<NutritionalAssessment?> GetById(Guid id);
    Task<List<NutritionalAssessment>> GetAll(Guid patientId, int? pageSize, int? pageNumber);
}