using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ApplicationTransaction> ApplicationTransactions => Set<ApplicationTransaction>();
    public DbSet<School> Schools => Set<School>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(builder =>
        {
            builder.HasKey(application => application.Id);
            builder.Property(application => application.Id).UseIdentityColumn();

            builder.Property(application => application.Submitted)
                   .HasConversion(time => time.ToUniversalTime(), time => DateTime.SpecifyKind(time, DateTimeKind.Utc));

            builder.HasOne(application => application.School)
                   .WithMany()
                   .HasForeignKey(application => application.SchoolId);
        });

        modelBuilder.Entity<School>(builder =>
        {
            builder.HasKey(school => school.Id);
            builder.Property(school => school.Id).UseIdentityColumn();
        });

        base.OnModelCreating(modelBuilder);
    }
}
