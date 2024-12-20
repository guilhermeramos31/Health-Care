using HealthCare.Models.EntityRole.Enum;

namespace HealthCare.Models.EntityEmployee.DTO;

public class EmployeeRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public Role Role { get; set; }
}
