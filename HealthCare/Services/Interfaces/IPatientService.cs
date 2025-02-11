using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Services.Interfaces;

public interface IPatientService
{
    Task<PatientResponseDto> Create(PatientRequestDto newPatient);
    Task<PatientResponseDto> Update(PatientRequestDto patient);
    Task<PatientResponseDto> GetPatient(string id);
    Task Delete(string cpf);
}