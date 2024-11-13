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

    internal DbSet<Media> Medias { get; set; }

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


    }

}
