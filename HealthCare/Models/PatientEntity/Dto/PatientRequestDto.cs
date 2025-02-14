using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.PatientEntity.Enum;
using Microsoft.Build.Framework;

namespace HealthCare.Models.PatientEntity.Dto;

public class PatientRequestDto
{
    [Required]
    public string Name { get; init; } = string.Empty;
    [Required]
    public string Nationality { get; init; } = string.Empty;
    [Required]
    public MaritalStatus MaritalStatus { get; init; }
    [Required]
    public string Cns { get; init; } = string.Empty;
    [Required]
    public string Rg { get; init; } = string.Empty;
    [Required]
    public string Cpf { get; init; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; init; }
    [Required]
    public AddressDto Address { get; init; } = new();
}