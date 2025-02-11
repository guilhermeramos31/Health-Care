using HealthCare.Models.PatientEntity;
using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<Patient> Create(Patient newPatient);
    Patient Update(Patient patient);
    Task<Patient?> FindByCpf(string cpf);
    Task<Patient?> GetPatient(string id);
    void Delete(Patient patient);
}