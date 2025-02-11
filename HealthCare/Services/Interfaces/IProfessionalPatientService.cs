using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Services.Interfaces;

public interface IProfessionalPatientService
{
    Task<ProfessionalPatient> Create(ProfessionalPatient professionalPatient);
    ProfessionalPatient Update(ProfessionalPatient professionalPatient);
    Task Delete(Guid id);
}