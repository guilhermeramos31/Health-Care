using HealthCare.Models.Enums;
using Microsoft.Build.Framework;

namespace HealthCare.Models.AddressEntity.Dto;

public class AddressDto
{
    [Required] public string Street { get; set; } = string.Empty;
    [Required] public string Number { get; set; } = string.Empty;
    public string? Complement { get; set; } = string.Empty;
    [Required] public string Neighborhood { get; set; } = string.Empty;
    [Required] public string City { get; set; } = string.Empty;
    [Required] public BrazilianState State { get; set; }
    [Required] public string ZipCode { get; set; } = string.Empty;
    public string? Landmark { get; set; } = string.Empty;
    [Required] public string AddressType { get; set; } = string.Empty;
}