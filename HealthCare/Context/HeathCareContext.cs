using HealthCare.Models.Employee;
using HealthCare.Models.Role;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Context;

public class HeathCareContext: IdentityDbContext<Employee, Role, Guid>
{
    public HeathCareContext( DbContextOptions<HeathCareContext> dbContextOptions ) : base( dbContextOptions) { }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        base.OnModelCreating( builder );


    }
}
