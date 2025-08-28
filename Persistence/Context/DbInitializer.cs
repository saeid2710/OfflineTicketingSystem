
using Entities.Ticket;
using Entities.Users;
using Shared.Enums;
using Shared.Utils;

namespace Persistence.Context
{
	public class DbInitializer
	{
		public static void Initialize(AppDbContext context)
		{
			context.Database.EnsureCreated();

			if (!context.Users.Any())
			{
				var users = new[]
				{
				new User
				{
					FullName = "Admin User",
					Email = "admin@company.com",
					PasswordHash =PasswordHelper.HashPassword("admin123"),
					Role = UserRole.Admin
				},
				new User
				{
					FullName = "Saeid doabi",
					Email = "Saeid@company.com",
					PasswordHash = PasswordHelper.HashPassword("emp123"),
					Role = UserRole.Employee
				},
				new User
				{
					FullName = "Reza Employee",
					Email = "Reza@company.com",
					PasswordHash = PasswordHelper.HashPassword("emp123"),
					Role = UserRole.Employee
				}
			};

				context.Users.AddRange(users);
				context.SaveChanges();
			}

			if (!context.Tickets.Any())
			{
				var employee = context.Users.First(u => u.Role == UserRole.Employee);
				var admin = context.Users.First(u => u.Role == UserRole.Admin);

				var tickets = new[]
				{
				new Ticket
				{
					Title = "Network Issue",
					Description = "Cannot connect to WiFi",
					Status = TicketStatus.Open,
					Priority = TicketPriority.High,
					CreatedByUserId = employee.Id
				},
				new Ticket
				{
					Title = "Software Installation",
					Description = "Need Photoshop installed",
					Status = TicketStatus.InProgress,
					Priority = TicketPriority.Medium,
					CreatedByUserId = employee.Id,
					AssignedToUserId = admin.Id
				}
			};

				context.Tickets.AddRange(tickets);
				context.SaveChanges();
			}
		}
	}
}
