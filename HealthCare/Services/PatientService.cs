using AutoMapper;
using HealthCare.Infrastructure.Configurations.Jwt;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Models.ProfessionalPatientEntity;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;
using HealthCare.Utils;
using Microsoft.Extensions.Options;

namespace HealthCare.Services;

public class PatientService(
    IRepositoryUow repositoryUow,
    IMapper mapper,
    IHttpContextAccessor accessor,
    IManagerUow managerUow,
    IProfessionalPatientService professionalPatientService,
    IOptions<JwtBody> jwtBody,
    ITokenService tokenService,
    IAddressService addressService) : IPatientService
{
    public async Task<PatientResponseDto> Create(PatientRequestDto newPatient)
    {
        var user = await accessor.GetEmployee(jwtBody, tokenService, managerUow);

        user = await managerUow.UserManager.FindByIdAsync(Convert.ToString(user.Id) ?? string.Empty) ??
               throw new BadHttpRequestException("User not found.");

        var patient = mapper.Map<Patient>(newPatient);
        patient = await repositoryUow.PatientRepository.Create(patient);
        await professionalPatientService.Create(new()
        {
            EmployeeId = user.Id,
            Employee = user,
            PatientId = patient.Id,
            Patient = patient
        });

        await repositoryUow.CommitAsync();
        return mapper.Map<PatientResponseDto>(await repositoryUow.PatientRepository.Create(patient));
    }

    public async Task<PatientResponseDto> Update(PatientRequestDto patientRequestDto)
    {
        var patient = await repositoryUow.PatientRepository.FindByCpf(patientRequestDto.Cpf) ??
                      throw new BadHttpRequestException("Patient not found.");

        patient.Name = patientRequestDto.Name;
        patient.DateOfBirth = patientRequestDto.DateOfBirth;
        patient.Address = addressService.Update(patientRequestDto.Address);
        patient.Nationality = patientRequestDto.Nationality;
        patient.MaritalStatus = patientRequestDto.MaritalStatus;

        repositoryUow.PatientRepository.Update(patient);
        await repositoryUow.CommitAsync();

        return mapper.Map<PatientResponseDto>(await repositoryUow.PatientRepository.Create(patient));
    }

    public async Task<PatientResponseDto> GetPatient(string id)
    {
        return mapper.Map<PatientResponseDto>(await repositoryUow.PatientRepository.GetPatient(id) ??
                                              throw new BadHttpRequestException("Patient not found."));
    }

    public async Task Delete(string cpf)
    {
        var patient = await repositoryUow.PatientRepository.FindByCpf(cpf) ??
                      throw new BadHttpRequestException("Patient not found.");
        repositoryUow.PatientRepository.Delete(patient);
        await repositoryUow.CommitAsync();
    }
}