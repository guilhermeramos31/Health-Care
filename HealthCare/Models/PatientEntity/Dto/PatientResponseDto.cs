using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.Enums;

namespace HealthCare.Models.PatientEntity.Dto;

public class PatientResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Nationality { get; init; } = string.Empty;
    public MaritalStatus MaritalStatus { get; init; }
    public DateTime DateOfBirth { get; init; }
    public DateTime AdmissionDate { get; init; }
    public AddressDto Address { get; init; } = new();
}