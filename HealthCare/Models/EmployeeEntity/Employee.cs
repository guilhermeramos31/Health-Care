using HealthCare.Models.ProfessionalPatientEntity;
using Microsoft.AspNetCore.Identity;
namespace HealthCare.Models.EmployeeEntity;

public class Employee : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    
    public List<ProfessionalPatient> ProfessionalPatients { get; set; } = new();

}
