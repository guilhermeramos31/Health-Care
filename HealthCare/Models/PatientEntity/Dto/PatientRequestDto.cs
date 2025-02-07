using System.Diagnostics.CodeAnalysis;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.Enums;
using Microsoft.Build.Framework;

namespace HealthCare.Models.PatientEntity.Dto;

public class PatientRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Nationality { get; set; } = string.Empty;
    [Required]
    public MaritalStatus MaritalStatus { get; set; }
    [Required]
    public string Cns { get; set; } = string.Empty;
    [Required]
    public string Rg { get; set; } = string.Empty;
    [Required]
    public string Cpf { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public Address Address { get; set; } = new();
}