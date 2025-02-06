using HealthCare.Models.Base;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.PatientEntity;

namespace HealthCare.Models.ProfessionalPatientEntity;

public class ProfessionalPatient : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
}