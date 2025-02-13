using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Services.Interfaces;

public interface IPatientService
{
    Task<PatientResponseDto> Create(PatientRequestDto newPatient);
    Task<PatientResponseDto> Update(Guid id, PatientUpdateDto updatePatient);
    Task<PatientResponseDto> GetPatient(Guid id);
    Task<Patient> GetPatientEntity(Guid id, bool asNoTracking = false);
    Task Delete(Guid patientId);
    Task<PatientPageResult> Patients(int page, int pageSize);
}