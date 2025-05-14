using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Models.Query;

namespace HealthCare.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<Patient> Create(Patient newPatient);
    Patient Update(Patient patient);
    Task<List<Patient>> FindByCpfOrCns(string cpf);
    Task<Patient?> GetPatient(Guid id, bool asNoTracking = false);
    void Delete(Patient patient);
    Task<PatientPage> GetPatients(Guid userId, QueryParameters queryParameters);
}