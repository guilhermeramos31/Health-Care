﻿namespace HealthCare.Services.Interfaces;

public interface IServiceUow
{
    IAddressService? AddressService { get; }
    IEmployeeRoleService? EmployeeRoleService { get; }
    IEmployeeService? EmployeeService { get; }
    IPatientService? PatientService { get; }
    IRoleService? RoleService { get; }
    ITokenService? TokenService { get; }
    IHealthSituationService? HealthSituationService { get; }
    IMedicationService? MedicationService { get; }
    INutritionalAssessmentService? NutritionalAssessmentService { get; }
}