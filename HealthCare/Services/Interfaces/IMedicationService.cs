using HealthCare.Models.MedicationEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface IMedicationService
{
    Task<MedicationResult> Create(Guid patientId, MedicationRequest medicationRequest);
    Task<MedicationResult> Update(Guid patientId, MedicationRequest medicationRequest);
    Task Delete(Guid medicationId);
    Task<MedicationResult> GetMedicationById(Guid medicationId);
    Task<MedicationPageResult> GetAllMedications(Guid patientId, int pageSize, int pageNumber);
}