using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.Enums;

namespace HealthCare.Models.PatientEntity.Dto;

public class PatientResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime AdmissionDate { get; set; }
    public AddressDto Address { get; set; } = new();
}