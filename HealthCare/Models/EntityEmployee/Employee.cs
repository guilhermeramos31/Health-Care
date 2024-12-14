using Microsoft.AspNetCore.Identity;
namespace HealthCare.Models.EntityEmployee;

public class Employee : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public ICollection<EntityRole.Role> Role { get; set; } = [ ];
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}
