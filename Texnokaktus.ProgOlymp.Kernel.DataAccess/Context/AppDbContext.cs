using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ApplicationTransaction> ApplicationTransactions => Set<ApplicationTransaction>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<School> Schools => Set<School>();
    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(builder =>
        {
            builder.HasKey(application => application.Id);
            builder.Property(application => application.Id).UseIdentityColumn();

            builder.Property(application => application.Submitted)
                   .HasConversion(time => time.ToUniversalTime(), time => DateTime.SpecifyKind(time, DateTimeKind.Utc));

            builder.HasOne(application => application.Participant)
                   .WithMany()
                   .HasForeignKey(application => application.ParticipantId);

            builder.HasOne(application => application.School)
                   .WithMany()
                   .HasForeignKey(application => application.SchoolId);

            builder.HasOne(application => application.Parent)
                   .WithMany()
                   .HasForeignKey(application => application.ParentId);

            builder.HasOne(application => application.Teacher)
                   .WithMany()
                   .HasForeignKey(application => application.TeacherId);
        });

        modelBuilder.Entity<Parent>(builder =>
        {
            builder.HasKey(parent => parent.Id);
            builder.Property(parent => parent.Id).UseIdentityColumn();
        });

        modelBuilder.Entity<Participant>(builder =>
        {
            builder.HasKey(participant => participant.Id);
            builder.Property(participant => participant.Id).UseIdentityColumn();
        });

        modelBuilder.Entity<School>(builder =>
        {
            builder.HasKey(school => school.Id);
            builder.Property(school => school.Id).UseIdentityColumn();
        });

        modelBuilder.Entity<Teacher>(builder =>
        {
            builder.HasKey(teacher => teacher.Id);
            builder.Property(teacher => teacher.Id).UseIdentityColumn();
        });

        base.OnModelCreating(modelBuilder);
    }
}
