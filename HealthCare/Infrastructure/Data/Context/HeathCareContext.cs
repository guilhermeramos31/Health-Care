using HealthCare.Migrations;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.HealthSituationEntity;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.ProfessionalPatientEntity;
using HealthCare.Models.RoleEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Data.Context;

public class HealthCareContext(DbContextOptions<HealthCareContext> options)
    : IdentityDbContext<Employee, Role, Guid>(options)
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<ProfessionalPatient> ProfessionalPatients { get; set; }
    public DbSet<HealthSituation> HealthSituations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProfessionalPatient>()
            .HasOne(pp => pp.Patient)
            .WithMany(p => p.ProfessionalPatients)
            .HasForeignKey(pp => pp.PatientId);

        builder.Entity<ProfessionalPatient>()
            .HasOne(pp => pp.Employee)
            .WithMany(p => p.ProfessionalPatients)
            .HasForeignKey(pp => pp.EmployeeId);

        builder.Entity<Patient>()
            .HasOne(p => p.Address)
            .WithMany(a => a.Patient)
            .HasForeignKey(p => p.AddressId);

        builder.Entity<HealthSituation>()
            .HasOne(hs => hs.Patient)
            .WithMany(p => p.HealthSituations)
            .HasForeignKey(hs => hs.PatientId);
    }
}