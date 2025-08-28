using Entities.Ticket;
using Shared.Dto;

namespace Contracts.TicketContract;

public interface ITicket
{
	Task<Guid?> CreateTicketAsync(Ticket ticket);
	Task<Guid?> UpdateTicketAsync(Ticket ticket);
	Task<IEnumerable<Ticket>> GetTicketByUserIdAsync(Guid userId);
	Task<IEnumerable<Ticket>> GetAllTicketAsync();
	Task<Ticket?> GetTicketByIdAsync(Guid ticketId);
	Task<List<StatDto>?> GetStatsAsync();
	Task<bool> RemoveTicketAsync(Ticket  ticket);
}