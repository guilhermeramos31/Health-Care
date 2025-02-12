using Microsoft.AspNetCore.Identity;

namespace HealthCare.Models.RoleEntity;

public class Role( string name ) : IdentityRole<Guid>( name ) { }
