using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityRole;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Context;

public class HeathCareContext( DbContextOptions<HeathCareContext> options ) : IdentityDbContext<Employee, Role, Guid>( options )
{
}
