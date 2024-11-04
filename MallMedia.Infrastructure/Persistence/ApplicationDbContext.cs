using MallMedia.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Persistence;
internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Device> Devices { get; set; }
    internal DbSet<Content> Contents { get; set; }
    internal DbSet<Schedule> Schedules { get; set; }

    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Location> Locations { get; set; }

    internal DbSet<Media> Media { get; set; }
    internal DbSet<TimeFrame> TimeFrames { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.Entity<Device>()
    //       .HasOne(d => d.User)
    //       .WithOne()
    //       .HasForeignKey<Device>(d => d.UserId);

    //    modelBuilder.Entity<Device>()
    //       .OwnsOne(d => d.Configuration, config =>
    //       {
    //           config.Property(c => c.Size).HasColumnName("Size");
    //           config.Property(c => c.Resolution).HasColumnName("Resolution");
    //       });

    //    modelBuilder.Entity<Device>()
    //      .HasOne<Location>()
    //      .WithMany()
    //      .HasForeignKey(c => c.LocationId);


    //    modelBuilder.Entity<Content>()
    //        .HasOne(c => c.Category)
    //        .WithMany(c => c.Contents)
    //        .HasForeignKey(c => c.CategoryId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<Content>()
    //            .HasMany(c => c.Media)
    //            .WithOne()
    //            .HasForeignKey(m => m.ContentId)
    //            .OnDelete(DeleteBehavior.Cascade);

    //    modelBuilder.Entity<Schedule>()
    //     .HasMany(s => s.TimeFrames)
    //     .WithMany(t => t.Schedules)
    //     .UsingEntity<Dictionary<string, object>>(
    //         "ScheduleTimeFrame", // Name of the join table
    //         j => j
    //             .HasOne<TimeFrame>()
    //             .WithMany()
    //             .HasForeignKey("TimeFrameId")
    //             .OnDelete(DeleteBehavior.Cascade),
    //         j => j
    //             .HasOne<Schedule>()
    //             .WithMany()
    //             .HasForeignKey("ScheduleId")
    //             .OnDelete(DeleteBehavior.Cascade)
    //     );

    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Device>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Device>(d => d.UserId);

        modelBuilder.Entity<Device>()
            .OwnsOne(d => d.Configuration, config =>
            {
                config.Property(c => c.Size).HasColumnName("Size");
                config.Property(c => c.Resolution).HasColumnName("Resolution");
            });

        modelBuilder.Entity<Device>()
            .HasOne(d => d.Location)
            .WithMany(l => l.Devices)
            .HasForeignKey(d => d.LocationId);

        modelBuilder.Entity<Content>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Contents)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict

        modelBuilder.Entity<Content>()
            .HasMany(c => c.Media)
            .WithOne()
            .HasForeignKey(m => m.ContentId)
            .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict

        modelBuilder.Entity<Schedule>()
            .HasMany(s => s.TimeFrames)
            .WithMany(t => t.Schedules)
            .UsingEntity<Dictionary<string, object>>(
                "ScheduleTimeFrame", // Name of the join table
                j => j
                    .HasOne<TimeFrame>()
                    .WithMany()
                    .HasForeignKey("TimeFrameId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Schedule>()
                    .WithMany()
                    .HasForeignKey("ScheduleId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }

}
