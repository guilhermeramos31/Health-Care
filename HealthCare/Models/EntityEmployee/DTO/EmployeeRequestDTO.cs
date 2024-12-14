namespace HealthCare.Models.EntityEmployee.DTO;

public class EmployeeRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public ICollection<string> RolesId { get; set; } = [ ];
}
