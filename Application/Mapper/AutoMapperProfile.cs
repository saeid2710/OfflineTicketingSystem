
using AutoMapper;
using Entities.Ticket;
using Entities.Users;
using Shared.Dto;

namespace Application.Mapper
{
	public class AutoMapperProfile:Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<CreateUserDto, User>().ReverseMap();
			CreateMap<TicketDto, Ticket>().ReverseMap();
			CreateMap<CreateUserDto, Ticket>().ReverseMap();
		}
	}
}
