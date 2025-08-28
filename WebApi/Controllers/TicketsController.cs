using Application.Services.TicketServices;
using Entities.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Enums;
using System.Security.Claims;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("tickets")]
	[Authorize]
	public class TicketsController : ControllerBase
	{
	   private readonly TicketServices _ticketServices;

	   public TicketsController(TicketServices ticketServices)
	   {
		   _ticketServices = ticketServices;
	   }
		[HttpPost("CreateTicket")]
		[Authorize(Roles = "Employee")]
		public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto dto)
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var id= await _ticketServices.CreateTicket(dto,userId);

			return CreatedAtAction(nameof(GetTicket), new { id = id }, dto);
		}

	
		[HttpGet("my")]
		[Authorize(Roles = "Employee")]
		public async Task<IActionResult> GetMyTickets()
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var tickets = await _ticketServices.GetUserTickets(userId);

			return Ok(tickets);
		}

		
		[HttpGet("GetAllTickets")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllTickets()
		{
			var tickets = await _ticketServices.GetAllTicketAsync();
			return Ok(tickets);
		}

		
		[HttpPut("UpdateTicket/{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketUpdateDto dto)
		{
			var ticket = await _ticketServices.UpdateTicket(dto, id);
			return Ok(ticket);
		}

		
		[HttpGet("stats")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetStats()
		{
			var stats = await _ticketServices.GetStats();

			return Ok(stats);
		}

		[HttpGet("GetTicket/{id}")]
		public async Task<IActionResult> GetTicket(Guid id)
		{
			var ticket = await _ticketServices.GetTicketById(id);

			if (ticket == null)
				return NotFound();

			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
			var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

			
			if (ticket.CreatedByUserId != userId && userRole != "Admin")
				return Forbid();
			return Ok(ticket);
		}

		
		[HttpDelete("DeleteTicket/{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteTicket(Guid id)
		{
			var res = await _ticketServices.RemoveTicketAsync(id);
			if (!res)
				return NotFound("تیکت پیدا نشد و یا در فرایند حذف تیکت خطایی رخ داده است");

			return Ok();
		}
	}
}
