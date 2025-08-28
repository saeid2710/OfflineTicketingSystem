
using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace Shared.Dto
{
	public class TicketUpdateDto
	{
		[Required]
		public TicketStatus Status { get; set; }
		[Required]
		public Guid AssignedToUserId { get; set; }
	}
}
