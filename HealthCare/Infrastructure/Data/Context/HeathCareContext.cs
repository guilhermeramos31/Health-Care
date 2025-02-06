using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Context;

public class HeathCareContext( DbContextOptions<HeathCareContext> options ) : IdentityDbContext<Employee, Role, Guid>( options )
{
}
