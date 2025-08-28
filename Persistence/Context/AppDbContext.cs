
using Entities.Ticket;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.CreatedByUser)
				.WithMany(u => u.CreatedTickets)
				.HasForeignKey(t => t.CreatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.AssignedToUser)
				.WithMany(u => u.AssignedTickets)
				.HasForeignKey(t => t.AssignedToUserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
