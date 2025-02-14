using HealthCare.Models.Base;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Enum;

namespace HealthCare.Models.AddressEntity;

public class Address : BaseEntity
{
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public BrazilianState State { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public string? Landmark { get; set; } = string.Empty;
    public string AddressType { get; set; } = string.Empty;

    public List<Patient> Patient { get; set; } = new();
}