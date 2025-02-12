using AutoMapper;
using HealthCare.Infrastructure.Configurations.Jwt;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;
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

    public async Task<PatientResponseDto> Update(Guid patientId, PatientUpdateDto updatePatient)
    {
        var patient = await repositoryUow.PatientRepository.GetPatient(patientId);
        if (patient == null) throw new BadHttpRequestException("Patient not found.");
        patient = mapper.Map(updatePatient, patient);
        patient.Address = await addressService.Update(patient.AddressId, updatePatient.Address);

        repositoryUow.PatientRepository.Update(patient);
        await repositoryUow.CommitAsync();

        return mapper.Map<PatientResponseDto>(await repositoryUow.PatientRepository.Create(patient));
    }

    public async Task<PatientResponseDto> GetPatient(Guid id)
    {
        return mapper.Map<PatientResponseDto>(await repositoryUow.PatientRepository.GetPatient(id) ??
                                              throw new BadHttpRequestException("Patient not found."));
    }

    public async Task Delete(Guid patientId)
    {
        var patient = await repositoryUow.PatientRepository.GetPatient(patientId);
        if (patient == null) throw new BadHttpRequestException("Patient not found.");

        await addressService.Delete(patient.AddressId);
        repositoryUow.PatientRepository.Delete(patient);

        await repositoryUow.CommitAsync();
    }

    public async Task<PatientPageResult> Patients(int page, int pageSize)
    {
        var user = await accessor.GetEmployee(jwtBody, tokenService, managerUow);
        user = await managerUow.UserManager.FindByIdAsync(Convert.ToString(user.Id) ?? string.Empty) ??
               throw new BadHttpRequestException("User not found.");

        var patients = await repositoryUow.PatientRepository.GetPatients(user.Id, page, pageSize);

        return new()
        {
            Patients = mapper.Map<List<PatientResponseDto>>(patients),
            TotalCount = patients.Count
        };
    }
}