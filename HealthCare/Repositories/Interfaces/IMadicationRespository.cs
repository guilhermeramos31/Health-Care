using HealthCare.Models.MedicationEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IMedicationRepository
{
    Task<Medication> Create(Medication newMedication);
    Medication Update(Medication updateMedication);
    void Delete(Medication medication);
    Task<Medication?> GetMedicationById(Guid medicationId);
    Task<List<Medication>> GetAllMedications(Guid patientId, int? pageSize, int? pageNumber);
}