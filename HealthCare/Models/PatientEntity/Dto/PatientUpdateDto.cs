using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.Enums;
using Microsoft.Build.Framework;

namespace HealthCare.Models.PatientEntity.Dto;

public class PatientUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Nationality { get; set; } = string.Empty;
    [Required]
    public MaritalStatus MaritalStatus { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public AddressDto Address { get; set; } = new();
}