using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurer Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                // Navn skal være unikt
                entity.HasIndex(r => r.Name).IsUnique();
            });

            // Konfigurer User entity
            modelBuilder.Entity<User>(entity =>
            {
                // Email skal være unikt
                entity.HasIndex(u => u.Email).IsUnique();

                // Konfigurer foreign key til Role
                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<Hotel>()
            .Property(h => h.Id)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Rooms)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);

            // Seed roller og test brugere (kun til udvikling)
            SeedRoles(modelBuilder);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {

            var roles = new[]
            {
                new Role
                {
                   
                    Id = 1,
                    Name = "User",
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Role
                {
                    Id = 2,
                    Name = "CleaningStaff",
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Role
                {
                    Id = 3,
                    Name = "Reception",
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Role
                {
                    Id = 4,
                    Name = "Admin",
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                }
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}