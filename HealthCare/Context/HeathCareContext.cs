using HealthCare.Models.EntityEmployee;
using HealthCare.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Context;

public class HeathCareContext: IdentityDbContext<Employee, EntityRole, Guid>
{
    public HeathCareContext( DbContextOptions<HeathCareContext> options ) : base( options ) { }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        base.OnModelCreating( builder );

        builder.Entity<IdentityUserRole<Guid>>( entity =>
        {
            entity.ToTable( "UserRoles" );
        } );

        builder.Entity<EntityRole>( entity =>
        {
            entity.ToTable( "Roles" );
            entity.HasKey( r => r.Id );
            entity.Property( r => r.Name ).HasMaxLength( 256 );
            entity.Property( r => r.NormalizedName ).HasMaxLength( 256 );
            entity.HasIndex( r => r.NormalizedName ).HasDatabaseName( "RoleNameIndex" );
        } );

        builder.Entity<EntityRole>().HasData( new { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" }, new { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" } );

        builder.Entity<Employee>( entity =>
        {
            entity.ToTable( "Employees" );
            entity.HasKey( e => e.Id );
            entity.Property( e => e.Name ).HasMaxLength( 256 );
            entity.Property( e => e.NormalizedUserName ).HasMaxLength( 256 );
            entity.Property( e => e.NormalizedEmail ).HasMaxLength( 256 );
            entity.HasIndex( e => e.NormalizedUserName ).HasDatabaseName( "UserNameIndex" );
            entity.HasIndex( e => e.NormalizedEmail ).HasDatabaseName( "EmailIndex" );
        } );
    }
}
