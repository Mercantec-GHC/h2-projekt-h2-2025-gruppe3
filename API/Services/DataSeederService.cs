using API.Data;
using Bogus;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// Service til at seede databasen med test data ved hjælp af Bogus faker library.
    /// Genererer realistiske test data for brugere, hoteller, rum og bookinger.
    /// </summary>
    public class DataSeederService
    {
        private readonly AppDBContext _context;
        private readonly ILogger<DataSeederService> _logger;

        public DataSeederService(AppDBContext context, ILogger<DataSeederService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Seeder databasen med komplet test data.
        /// </summary>
        /// <param name="userCount">Antal brugere at oprette</param>
        /// <param name="hotelCount">Antal hoteller at oprette</param>
        /// <param name="roomsPerHotel">Antal rum per hotel</param>
        /// <param name="bookingCount">Antal bookinger at oprette</param>
        public async Task<string> SeedDatabaseAsync(int userCount = 50, int hotelCount = 10, int roomsPerHotel = 20, int bookingCount = 100)
        {
            try
            {
                var summary = new StringBuilder();
                _logger.LogInformation("Starter database seeding...");

                // Tjek om der allerede er data
                //var existingUsers = await _context.Users.CountAsync();
                var existingHotels = await _context.Hotels.CountAsync();
                var existingRooms = await _context.Rooms.CountAsync();
                //var existingBookings = await _context.Bookings.CountAsync();

                summary.AppendLine($"Eksisterende data før seeding:");
                //summary.AppendLine($"- Brugere: {existingUsers}");
                summary.AppendLine($"- Hoteller: {existingHotels}");
                summary.AppendLine($"- Rum: {existingRooms}");
                //summary.AppendLine($"- Bookinger: {existingBookings}");
                summary.AppendLine();

                //// Sikr at der findes roller i databasen
                //await EnsureRolesExistAsync();
                //summary.AppendLine("✅ Roller sikret");

                //// Seed brugere
                //var users = await SeedUsersAsync(userCount);
                //summary.AppendLine($"✅ Oprettet {users.Count} brugere");

                // Seed hoteller
                var hotels = await SeedHotelsAsync(hotelCount);
                summary.AppendLine($"✅ Oprettet {hotels.Count} hoteller");

                // Seed rum
                var rooms = await SeedRoomsAsync(hotels, roomsPerHotel);
                summary.AppendLine($"✅ Oprettet {rooms.Count} rum");

                //// Seed bookinger
                //var bookings = await SeedBookingsAsync(users, rooms, bookingCount);
                //summary.AppendLine($"✅ Oprettet {bookings.Count} bookinger");

                summary.AppendLine();
                summary.AppendLine("🎉 Database seeding fuldført succesfuldt!");

                _logger.LogInformation("Database seeding fuldført succesfuldt");
                return summary.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl under database seeding");
                throw;
            }
        }



        /// <summary>
        /// Opretter fake hoteller med realistiske data.
        /// </summary>
        private async Task<List<Hotel>> SeedHotelsAsync(int count)
        {
            var hotels = new List<Hotel>();

            var hotelNames = new[]
            {
                "Hotel Royal", "Grand Hotel", "Scandic", "Best Western", "Radisson Blu",
                "Hotel Alexandra", "Nimb Hotel", "Hotel d'Angleterre", "Copenhagen Marriott",
                "Clarion Hotel", "Comfort Hotel", "First Hotel", "Cabinn Hotel", "Wakeup Hotel",
                "Hotel Phoenix", "Hotel Kong Arthur", "Hotel Sanders", "71 Nyhavn Hotel",
                "Hotel Skt. Petri", "AC Hotel", "Villa Copenhagen", "Hotel SP34"
            };

            var danishCities = new[]
            {
                "København", "Aarhus", "Odense", "Aalborg", "Esbjerg",
                "Randers", "Kolding", "Horsens", "Vejle", "Roskilde",
                "Herning", "Silkeborg", "Næstved", "Fredericia", "Viborg"
            };

            var danishStreets = new[]
            {
                "Nørregade", "Vestergade", "Østergade", "Søndergade", "Hovedgade",
                "Kongens Nytorv", "Strøget", "Nyhavn", "Amaliegade", "Bredgade",
                "Store Kongensgade", "Gothersgade", "Sankt Peders Stræde"
            };

            for (int i = 0; i < count; i++)
            {
                var baseFaker = new Faker();
                var hotelName = baseFaker.PickRandom(hotelNames) + " " + baseFaker.PickRandom(danishCities);

                // Sikr unikt navn
                var counter = 1;
                var originalName = hotelName;
                while (hotels.Any(h => h.Name == hotelName))
                {
                    hotelName = originalName + " " + counter;
                    counter++;
                }

                var hotel = new Hotel
                {
                    Name = hotelName,
                    Road = baseFaker.PickRandom(danishStreets) + " " + baseFaker.Random.Int(1, 200),
                    Zip = baseFaker.Random.Int(1000, 9999).ToString(),
                    City = baseFaker.PickRandom(danishCities),
                    Country = "Danmark",
                    CreatedAt = baseFaker.Date.Between(DateTime.UtcNow.AddYears(-5), DateTime.UtcNow.AddYears(-1)),
                    UpdatedAt = DateTime.UtcNow
                };

                hotel.UpdatedAt = baseFaker.Date.Between(hotel.CreatedAt, DateTime.UtcNow);
                hotels.Add(hotel);
            }

            _context.Hotels.AddRange(hotels);
            await _context.SaveChangesAsync();

            return hotels;
        }

        /// <summary>
        /// Opretter fake rum for hvert hotel.
        /// </summary>
        private async Task<List<Room>> SeedRoomsAsync(List<Hotel> hotels, int roomsPerHotel)
        {
            var rooms = new List<Room>();

            foreach (var hotel in hotels)
            {
                var faker = new Faker<Room>("en")
                    .RuleFor(r => r.RoomNumber, f => f.Random.Int(101, 999))
                    .RuleFor(r => r.RoomtypeId, f => f.Random.WeightedRandom(new[] { 1, 2, 3, 4, 6 }, new[] { 0.1f, 0.5f, 0.2f, 0.15f, 0.05f }))
                    .RuleFor(r => r.HotelId, f => hotel.Id)
                    .RuleFor(r => r.CreatedAt, f => f.Date.Between(hotel.CreatedAt, DateTime.UtcNow))
                    .RuleFor(r => r.UpdatedAt, (f, r) => f.Date.Between(r.CreatedAt, DateTime.UtcNow));

                var hotelRooms = faker.Generate(roomsPerHotel);

                // Sikr unikke rum numre per hotel
                var usedNumbers = new HashSet<int>();
                foreach (var room in hotelRooms)
                {
                    while (usedNumbers.Contains(room.RoomNumber))
                    {
                        room.RoomNumber = new Faker().Random.Int(101, 999);
                    }
                    usedNumbers.Add(room.RoomNumber);
                }

                rooms.AddRange(hotelRooms);
            }

            _context.Rooms.AddRange(rooms);
            await _context.SaveChangesAsync();

            return rooms;
        }


        /// <summary>
        /// Seeder kun bookinger baseret på eksisterende brugere og rum.
        /// </summary>
        /// <param name="bookingCount">Antal bookinger at oprette</param>
        /// <returns>Seeding resultat</returns>
        //public async Task<string> SeedBookingsOnlyAsync(int bookingCount = 50)
        //{
        //    try
        //    {
        //        var summary = new StringBuilder();
        //        _logger.LogInformation("Starter booking-only seeding...");

        //        // Hent eksisterende brugere og rum
        //        var existingUsers = await _context.Users.ToListAsync();
        //        var existingRooms = await _context.Rooms.Include(r => r.Bookings).ToListAsync();

        //        if (!existingUsers.Any())
        //        {
        //            throw new InvalidOperationException("Ingen brugere fundet i databasen. Seed brugere først.");
        //        }

        //        if (!existingRooms.Any())
        //        {
        //            throw new InvalidOperationException("Ingen rum fundet i databasen. Seed hoteller og rum først.");
        //        }

        //        summary.AppendLine($"Fundet {existingUsers.Count} brugere og {existingRooms.Count} rum");

        //        // Seed bookinger
        //        var bookings = await SeedBookingsAsync(existingUsers, existingRooms, bookingCount);
        //        summary.AppendLine($"✅ Oprettet {bookings.Count} nye bookinger");

        //        summary.AppendLine();
        //        summary.AppendLine("🎉 Booking seeding fuldført succesfuldt!");

        //        _logger.LogInformation("Booking seeding fuldført succesfuldt");
        //        return summary.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Fejl under booking seeding");
        //        throw;
        //    }
        //}

        /// <summary>
        /// Rydder alle data fra databasen.
        /// </summary>
        public async Task<string> ClearDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("Rydder database...");

                //var bookingCount = await _context.Bookings.CountAsync();
                var roomCount = await _context.Rooms.CountAsync();
                var hotelCount = await _context.Hotels.CountAsync();
                //var userCount = await _context.Users.CountAsync();

                //_context.Bookings.RemoveRange(_context.Bookings);
                _context.Rooms.RemoveRange(_context.Rooms);
                _context.Hotels.RemoveRange(_context.Hotels);
                //_context.Users.RemoveRange(_context.Users);

                await _context.SaveChangesAsync();

                var summary = $"🗑️ Database ryddet!\n" +
                             //$"- Slettet {bookingCount} bookinger\n" +
                             $"- Slettet {roomCount} rum\n" +
                             $"- Slettet {hotelCount} hoteller\n";
                //$"- Slettet {userCount} brugere";

                _logger.LogInformation("Database ryddet succesfuldt");
                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved rydning af database");
                throw;
            }
        }

        /// <summary>
        /// Henter database statistikker.
        /// </summary>
        public async Task<object> GetDatabaseStatsAsync()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var adminCount = adminRole != null ? await _context.Users.CountAsync(u => u.RoleId == adminRole.Id) : 0;

            return new
            {
                //Users = await _context.Users.CountAsync(),
                //AdminUsers = adminCount,
                Hotels = await _context.Hotels.CountAsync(),
                Rooms = await _context.Rooms.CountAsync(),
                //Bookings = await _context.Bookings.CountAsync(),
                //ActiveBookings = await _context.Bookings.CountAsync(b => b.BookingStatus == "Confirmed" || b.BookingStatus == "CheckedIn"),
                LastSeeded = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Hash password helper metode.
        /// </summary>
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
