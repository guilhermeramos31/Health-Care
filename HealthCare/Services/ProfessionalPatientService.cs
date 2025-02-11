using HealthCare.Models.ProfessionalPatientEntity;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class ProfessionalPatientService(IRepositoryUow repositoryUow)
    : IProfessionalPatientService
{
    public async Task<ProfessionalPatient> Create(ProfessionalPatient professionalPatient)
    {
        return await repositoryUow.ProfessionalPatientRepository.Create(professionalPatient);
    }

    public ProfessionalPatient Update(ProfessionalPatient professionalPatient)
    {
        return repositoryUow.ProfessionalPatientRepository.Update(professionalPatient) ??
               throw new BadHttpRequestException("Patient not found.");
    }

    public async Task Delete(Guid id)
    {
        var patient = await repositoryUow.ProfessionalPatientRepository.GetByPatientId(id) ??
                      throw new BadHttpRequestException("Patient not found.");
        repositoryUow.ProfessionalPatientRepository.Delete(patient);
    }
}