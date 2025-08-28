
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
	public class TicketDto:BaseDto
	{

		public string Title { get; set; } 
		public string Description { get; set; }
		public TicketStatus Status { get; set; } 
		public TicketPriority Priority { get; set; } 


		public Guid CreatedByUserId { get; set; }
		public Guid? AssignedToUserId { get; set; }


		public UserDto CreatedByUser { get; set; } = null!;
		public UserDto? AssignedToUser { get; set; }
	}
}
