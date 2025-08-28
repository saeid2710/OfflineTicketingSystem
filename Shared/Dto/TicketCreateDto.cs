
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
	public class TicketCreateDto
	{
		[Required]
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public TicketPriority Priority { get; set; } = TicketPriority.Medium;
	}
}
