
using Contracts.TicketContract;
using Entities.Ticket;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Shared.Dto;

namespace Persistence.Repository
{
	public class TicketRepository : ITicket
	{
		private readonly AppDbContext _db;
		public TicketRepository(AppDbContext db) => _db = db;


		public async Task<Guid?> CreateTicketAsync(Ticket ticket)
		{
			ticket.CreatedAt=DateTime.Now;
			ticket.UpdateAt=DateTime.Now;
			ticket.IsDelete	=false;
			await _db.Tickets.AddAsync(ticket);
			await _db.SaveChangesAsync();
			return ticket.Id;
		}

		public async Task<Guid?> UpdateTicketAsync(Ticket ticket)
		{
			ticket.UpdateAt=DateTime.Now;
			_db.Update(ticket);
			await _db.SaveChangesAsync();
			return ticket.Id;
		}

		public async Task<IEnumerable<Ticket>> GetTicketByUserIdAsync(Guid userId)
		{
			var tickets = await _db.Tickets.AsNoTracking().AsSplitQuery()
				.Where(t => t.CreatedByUserId == userId)
				.Include(t => t.AssignedToUser)
				.OrderByDescending(t => t.CreatedAt)
				.ToListAsync();
			return tickets;
		}

		public async Task<IEnumerable<Ticket>> GetAllTicketAsync()
		{
			return await _db.Tickets.AsNoTracking().AsSplitQuery()
				.Include(x => x.AssignedToUser)
				.Include(x => x.CreatedByUser)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();
		}

		public async Task<Ticket?> GetTicketByIdAsync(Guid ticketId)
		{
			return await _db.Tickets.SingleOrDefaultAsync(x=>x.Id==ticketId);
		}

		public async Task<List<StatDto>?> GetStatsAsync()
		{
			return await _db.Tickets
				.GroupBy(t => t.Status)
				.Select(g => new StatDto { Status = g.Key.ToString(), Count = g.Count() })
				.ToListAsync();
		}

		public async Task<bool> RemoveTicketAsync(Ticket ticket)
		{
			ticket.IsDelete=true;
			_db.Update(ticket);
			await _db.SaveChangesAsync();
			return true;
		}
	}
}
