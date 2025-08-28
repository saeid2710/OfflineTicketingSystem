
using Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace Shared.Dto
{
	public class UserDto:BaseDto
	{

	
		public string FullName { get; set; } 

		public string Email { get; set; } 

		public string PasswordHash { get; set; }

		public UserRole Role { get; set; } 




		public ICollection<TicketDto> CreatedTickets { get; set; } = new List<TicketDto>();
		public ICollection<TicketDto> AssignedTickets { get; set; } = new List<TicketDto>();
	}
}
