
using AutoMapper;
using Contracts.TicketContract;
using Contracts.UserContract;
using Entities.Ticket;
using Shared.Dto;

namespace Application.Services.TicketServices
{
	public class TicketServices
	{
		private readonly ITicket _ticketRepository;
		private readonly IUser _userRepository;
		private readonly IMapper _mapper;

		public TicketServices(ITicket ticketRepository, IUser userRepository, IMapper mapper)
		{
			_ticketRepository = ticketRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<List<TicketDto>> GetAllTicketAsync()
		{
			var ticketList = await _ticketRepository.GetAllTicketAsync();
			return _mapper.Map<List<TicketDto>>(ticketList);
		}

		public async Task<TicketDto> GetTicketById(Guid ticketId)
		{
			var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
			if (ticket is null)
				throw new Exception("تیکت پیدا نشد");
			return _mapper.Map<TicketDto>(ticket);

		}

		public async Task<List<TicketDto>> GetUserTickets(Guid userId)
		{
			var user = await _userRepository.GetUserByIdAsync(userId);
			if (user is null)
				throw new Exception("کاربری با این مشخصات یافت نشد");
			var tickets = await _ticketRepository.GetTicketByUserIdAsync(userId);
			return _mapper.Map<List<TicketDto>>(tickets);
		}

		public async Task<Guid?> CreateTicket(TicketCreateDto dto,Guid userId)
		{
			var ticket = new Ticket
			{

				Title = dto.Title,
				Description = dto.Description,
				Priority = dto.Priority,
				CreatedByUserId = userId,
				AssignedToUserId = userId
			};
			return await _ticketRepository.CreateTicketAsync(ticket);
		}

		public async Task<List<StatDto>?> GetStats()
		{
			return await _ticketRepository.GetStatsAsync();
		}
		public async Task<Guid?> UpdateTicket(TicketUpdateDto dto, Guid ticketId)
		{
			var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
			if (ticket is null)
				throw new Exception("تیکت پیدا نشد");

			ticket.Status = dto.Status;
			ticket.AssignedToUserId = dto.AssignedToUserId;
			return await _ticketRepository.UpdateTicketAsync(ticket);
		}

		public async Task<bool> RemoveTicketAsync(Guid ticketId)
		{
			var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
			if (ticket is null)
				throw new KeyNotFoundException("تیکت مورد نظر پیدا نشد");
			return await _ticketRepository.RemoveTicketAsync(ticket);
		}
	}
}
