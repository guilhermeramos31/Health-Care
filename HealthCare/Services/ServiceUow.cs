﻿using AutoMapper;
using HealthCare.Infrastructure.Configurations.Jwt;
using HealthCare.Infrastructure.Configurations.Jwt.Interfaces;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace HealthCare.Services;

public class ServiceUow(
    IRepositoryUow repositoryUow,
    IMapper mapper,
    IManagerUow managerUow,
    IJwt jwt,
    IHttpContextAccessor accessor,
    IOptions<JwtBody> jwtOptions) : IServiceUow
{
    private IAddressService? _addressService;
    private IEmployeeRoleService? _employeeRoleService;
    private IEmployeeService? _employeeService;
    private IPatientService? _patientService;
    private IRoleService? _roleService;
    private ITokenService? _tokenService;
    private IProfessionalPatientService? _professionalPatientService;
    private IHealthSituationService? _healthSituationService;
    private IMedicationService? _medicationService;
    private INutritionalAssessmentService? _nutritionalAssessmentService;

    public IAddressService AddressService => _addressService ??= new AddressService(repositoryUow, mapper);
    public IEmployeeRoleService EmployeeRoleService => _employeeRoleService ??= new EmployeeRoleService(managerUow);

    private IProfessionalPatientService ProfessionalPatientService =>
        _professionalPatientService ??= new ProfessionalPatientService(repositoryUow);

    public IEmployeeService EmployeeService => _employeeService ??=
        new EmployeeService(mapper, TokenService, managerUow, EmployeeRoleService, jwt);

    public IPatientService PatientService =>
        _patientService ??=
            new PatientService(repositoryUow, mapper, accessor, managerUow, ProfessionalPatientService, jwtOptions,
                TokenService, AddressService);

    public IHealthSituationService HealthSituationService => _healthSituationService ??=
        new HealthSituationService(mapper, repositoryUow, PatientService);

    public IRoleService RoleService => _roleService ??= new RoleService(managerUow, mapper);
    public ITokenService TokenService => _tokenService ??= new TokenService(managerUow, accessor, jwt, jwtOptions);

    public IMedicationService MedicationService =>
        _medicationService ??= new MedicationService(mapper, repositoryUow, PatientService);

    public INutritionalAssessmentService NutritionalAssessmentService => _nutritionalAssessmentService ??=
        new NutritionalAssessmentService(mapper, repositoryUow, PatientService);
}