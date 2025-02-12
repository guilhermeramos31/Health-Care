using HealthCare.Models.AddressEntity;
using HealthCare.Models.Base;
using HealthCare.Models.Enums;
using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Models.PatientEntity;

public class Patient : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public MaritalStatus MaritalStatus { get; set; }
    public string Cns { get; set; } = string.Empty;
    public string Rg { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime AdmissionDate { get; set; }

    public Guid AddressId { get; set; }
    public Address Address { get; set; } = new();
    public List<ProfessionalPatient> ProfessionalPatients { get; set; } = new();
}