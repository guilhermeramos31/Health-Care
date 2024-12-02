using HealthCare.Models.EntityEmployee;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Models.Role;

public class EntityRole: IdentityRole<Guid>
{
    public EntityRole() { }
    public EntityRole( string roleName ) : base( roleName ) { }
    public ICollection<Employee> UserId { get; set; } = [];
}
