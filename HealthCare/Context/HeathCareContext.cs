using HealthCare.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Context;

public class HeathCareContext: IdentityDbContext
{
    public HeathCareContext( DbContextOptions<HeathCareContext> dbContextOptions ) : base( dbContextOptions) { }

    public required DbSet<Employee> Employees {  get; set; }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        base.OnModelCreating( builder );
    }
}
