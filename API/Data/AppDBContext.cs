using DomainModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

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
        public DbSet<Facility> Facilities { get; set; } = null!;
        public DbSet<Roomtype> Roomtypes { get; set; } = null!;

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


            modelBuilder.Entity<Facility>()
                .HasKey(h => h.HotelId); // Shared PK


            modelBuilder.Entity<Hotel>()
                .HasOne(u => u.Facility)
                .WithOne(i => i.Hotel)
                .HasForeignKey<Facility>(i => i.HotelId);


            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);


            modelBuilder.Entity<Roomtype>()
                .HasMany(t => t.Rooms)
                .WithOne(r => r.Roomtype)
                .HasForeignKey(r => r.RoomtypeId);


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId);

            // Seed roller og test brugere (kun til udvikling)
            SeedRoles(modelBuilder);
            SeedUser(modelBuilder);
            SeedRoomtype(modelBuilder);
            SeedHotel(modelBuilder);
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

        private void SeedUser(ModelBuilder modelBuilder)
        {
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.com",
                    HashedPassword = "$2a$11$BJtEDbA0yeNpnSNKPeGh7eCmVA6tIUoC.QLBFqMjGh.7MWUSGtKJe",
                    PasswordBackdoor = "!MyVerySecureSecretKeyThatIsAtLeast32CharactersLong123456789",
                    RoleId = 4,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
            };
            modelBuilder.Entity<User>().HasData(users);
        }

        private void SeedRoomtype(ModelBuilder modelBuilder)
        {
            var roomtypes = new[]
            {
                new Roomtype
                {
                    Id = 1,
                    Name = "Enkeltværelse",
                    Description = "Et enkeltværelse med én seng, ideelt til én person.",
                    BasePrice = 2999.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Roomtype
                {
                    Id = 2,
                    Name = "Dobbeltværelse",
                    Description = "Et dobbeltværelse med to senge eller en dobbeltseng.",
                    BasePrice = 3299.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Roomtype
                {
                    Id = 3,
                    Name = "Suite",
                    Description = "En suite med ekstra plads og komfort, ofte med separat opholdsområde.",
                    BasePrice = 3399.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Roomtype
                {
                    Id = 4,
                    Name = "Familieværelse",
                    Description = "Et værelse med plads til hele familien, typisk med flere senge.",
                    BasePrice = 3499.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Roomtype
                {
                    Id = 5,
                    Name = "Deluxe værelse",
                    Description = "Et deluxe værelse med ekstra faciliteter og komfort.",
                    BasePrice = 3599.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Roomtype
                {
                    Id = 6,
                    Name = "Handicapvenligt værelse",
                    Description = "Et værelse designet til gæster med særlige behov og nem adgang.",
                    BasePrice = 3199.99,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                }
            };
            modelBuilder.Entity<Roomtype>().HasData(roomtypes);
        }

        private void SeedHotel(ModelBuilder modelBuilder)
        {
            var hotels = new[]
            {
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel 1",
                    Road = "H. C. Andersens Vej 9",
                    Zip = "8800",
                    City = "Viborg",
                    Country = "Danmark",
                    Phone = 12345678,
                    Email = "mercantec@mercantec.dk",
                    Description = "First Central Hotel Suites er udstyret med 524 moderne suiter, der kan prale af moderne finish og en lokkende hyggelig stemning, der giver hver gæst den ultimative komfort og pusterum. Hotellet tilbyder en bred vifte af fritids- og forretningsfaciliteter, herunder et mini-businesscenter, rejseskrivebord, en fredfyldt pool på taget, veludstyret fitnesscenter og rekreative faciliteter.\r\nFra spisning til roomservice, oplev en balance mellem kontinentale retter og tilfredsstil dine trang med den friske gane i Beastro Restaurant og den søde duft af kaffe på Beastro, der ligger i lobbyen.",
                    PercentagePrice = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel 2",
                    Road = "H. C. Andersens Vej 9",
                    Zip = "8800",
                    City = "Viborg",
                    Country = "Danmark",
                    Phone = 12345678,
                    Email = "mercantec@mercantec.dk",
                    Description = "First Central Hotel Suites er udstyret med 524 moderne suiter, der kan prale af moderne finish og en lokkende hyggelig stemning, der giver hver gæst den ultimative komfort og pusterum. Hotellet tilbyder en bred vifte af fritids- og forretningsfaciliteter, herunder et mini-businesscenter, rejseskrivebord, en fredfyldt pool på taget, veludstyret fitnesscenter og rekreative faciliteter.\r\nFra spisning til roomservice, oplev en balance mellem kontinentale retter og tilfredsstil dine trang med den friske gane i Beastro Restaurant og den søde duft af kaffe på Beastro, der ligger i lobbyen.",
                    PercentagePrice = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel 3",
                    Road = "H. C. Andersens Vej 9",
                    Zip = "8800",
                    City = "Viborg",
                    Country = "Danmark",
                    Phone = 12345678,
                    Email = "mercantec@mercantec.dk",
                    Description = "First Central Hotel Suites er udstyret med 524 moderne suiter, der kan prale af moderne finish og en lokkende hyggelig stemning, der giver hver gæst den ultimative komfort og pusterum. Hotellet tilbyder en bred vifte af fritids- og forretningsfaciliteter, herunder et mini-businesscenter, rejseskrivebord, en fredfyldt pool på taget, veludstyret fitnesscenter og rekreative faciliteter.\r\nFra spisning til roomservice, oplev en balance mellem kontinentale retter og tilfredsstil dine trang med den friske gane i Beastro Restaurant og den søde duft af kaffe på Beastro, der ligger i lobbyen.",
                    PercentagePrice = 1,
                    CreatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc)
                },
            };
            modelBuilder.Entity<Hotel>().HasData(hotels);
        }
    }
}