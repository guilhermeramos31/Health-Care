using HealthCare.Models.AddressEntity;
using HealthCare.Models.Base;
using HealthCare.Models.Enums;
using HealthCare.Models.HealthSituationEntity;
using HealthCare.Models.ProfessionalPatientEntity;

namespace HealthCare.Models.PatientEntity;

public class Patient : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public string Nationality { get; init; } = string.Empty;
    public MaritalStatus MaritalStatus { get; init; }
    public string Cns { get; init; } = string.Empty;
    public string Rg { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public DateTime AdmissionDate { get; init; }

    public Guid AddressId { get; init; }
    public Address Address { get; set; } = new();
    public IList<ProfessionalPatient> ProfessionalPatients { get; init; } = new List<ProfessionalPatient>();
    public IList<HealthSituation> HealthSituations { get; init; } = new List<HealthSituation>();
}