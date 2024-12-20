using Microsoft.AspNetCore.Identity;

namespace HealthCare.Models.EntityRole;

public class Role( string name ) : IdentityRole<Guid>( name ) { }
