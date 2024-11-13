using MallMedia.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    internal DbSet<Media> Medias { get; set; }
    internal DbSet<TimeFrame> TimeFrames { get; set; }
    public DbSet<DeviceSchedule> DeviceSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Schedule>()
        .Ignore(s => s.Title);

        modelBuilder.Entity<Device>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Device>(d => d.UserId);

        modelBuilder.Entity<Device>()
            .OwnsOne(d => d.Configuration, config =>
            {
                config.Property(c => c.Size).HasColumnName("Size");
                config.Property(c => c.Resolution).HasColumnName("Resolution");
                config.Property(c => c.DeviceType).HasColumnName("DeviceType");
            });

        modelBuilder.Entity<Device>()
            .HasOne(d => d.Location)
            .WithMany()
            .HasForeignKey(d => d.LocationId);

        modelBuilder.Entity<Content>()
            .HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict

        modelBuilder.Entity<Content>()
            .HasMany(c => c.Media)
            .WithOne()
            .HasForeignKey(m => m.ContentId)
            .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict

        /*
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.TimeFrame)
            .WithMany(t => t.Schedules)
            .HasForeignKey(s => s.TimeFrameId);*/

        //modelBuilder.Entity<Schedule>()
        //    .HasMany(s => s.TimeFrames)
        //    .WithMany(t => t.Schedules)
        //    .UsingEntity<Dictionary<string, object>>(
        //        "ScheduleTimeFrame", // Name of the join table
        //        j => j
        //            .HasOne<TimeFrame>()
        //            .WithMany()
        //            .HasForeignKey("TimeFrameId")
        //            .OnDelete(DeleteBehavior.Cascade),
        //        j => j
        //            .HasOne<Schedule>()
        //            .WithMany()
        //            .HasForeignKey("ScheduleId")
        //            .OnDelete(DeleteBehavior.Cascade)
        //    );

    }

}
