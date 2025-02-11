using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IProfessionalPatientRepository
{
    Task<ProfessionalPatient> Create(ProfessionalPatient professionalPatient);
    ProfessionalPatient Update(ProfessionalPatient professionalPatient);
    void Delete(ProfessionalPatient professionalPatient);
    Task<ProfessionalPatient?> GetByPatientId(Guid patientId);
}