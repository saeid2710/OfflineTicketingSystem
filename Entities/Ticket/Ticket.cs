
using System.ComponentModel.DataAnnotations;
using Entities.Users;
using Shared.Enums;

namespace Entities.Ticket
{
	public class Ticket:Base.Entity
	{

		[Required]
		[MaxLength(200)]
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		[Required]
		public TicketStatus Status { get; set; } = TicketStatus.Open; 

		[Required]
		public TicketPriority Priority { get; set; } =TicketPriority.Medium; 

	
		public Guid CreatedByUserId { get; set; }
		public Guid? AssignedToUserId { get; set; }

	
		public User CreatedByUser { get; set; } = null!;
		public User? AssignedToUser { get; set; }
	}
}
