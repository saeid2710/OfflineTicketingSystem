
using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace Entities.Users
{
	public class User:Base.Entity
	{
		

		[Required]
		[MaxLength(100)]
		public string FullName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string PasswordHash { get; set; } = string.Empty;

		[Required]
		public UserRole Role { get; set; } = UserRole.None; 

		

	
		public ICollection<Ticket.Ticket> CreatedTickets { get; set; } = new List<Ticket.Ticket>();
		public ICollection<Ticket.Ticket> AssignedTickets { get; set; } = new List<Ticket.Ticket>();
	}
}
