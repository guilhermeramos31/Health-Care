using HealthCare.Models.Base;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.PatientEntity;

namespace HealthCare.Models.ProfessionalPatientEntity;

public class ProfessionalPatient : BaseEntity
{
    public Guid EmployeeId { get; init; }
    public Employee Employee { get; init; } = null!;

    public Guid PatientId { get; init; }
    public Patient Patient { get; init; } = null!;
}