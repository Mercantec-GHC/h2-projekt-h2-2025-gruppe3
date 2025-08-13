using DomainModels;
using Microsoft.EntityFrameworkCore;
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
		public DbSet<UserInfo> UserInfos { get; set; } = null!;
		public DbSet<Hotel> Hotels { get; set; } = null!;
		public DbSet<Room> Rooms { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// 1:n: User -> Role
			modelBuilder.Entity<User>()
				.HasOne(u => u.Role)
				.WithMany(r => r.Users)
				.HasForeignKey(u => u.RoleId)
				.OnDelete(DeleteBehavior.Restrict);
			// undgå at slette rolle hvis der findes users

			// (Nice-to-have) Unikt navn på roller
			modelBuilder.Entity<Role>()
				.HasIndex(r => r.Name)
				.IsUnique();

			modelBuilder.Entity<UserInfo>()
				.HasKey(i => i.UserId); // Shared PK

			modelBuilder.Entity<User>()
				.HasOne(u => u.Info)
				.WithOne(i => i.User)
				.HasForeignKey<UserInfo>(i => i.UserId);

			modelBuilder.Entity<Hotel>()
				.HasMany(h => h.Rooms)
				.WithOne(r => r.Hotel)
				.HasForeignKey(r => r.HotelId);

			modelBuilder.Entity<Room>()
				.HasOne(r => r.Hotel)
				.WithMany(h => h.Rooms)
				.HasForeignKey(r => r.HotelId);
		}
	}
}
