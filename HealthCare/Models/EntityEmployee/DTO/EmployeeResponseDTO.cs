using HealthCare.Models.Role.DTO;

namespace HealthCare.Models.EntityEmployee.DTO;

public class EmployeeResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public ICollection<RoleResponseDTO> RoleIds { get; set; } = [ ];
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
