
using AutoMapper;
using Contracts.UserContract;
using Shared.Dto;
using Shared.Utils;

namespace Application.Services.AuthServices
{
	public class AuthService
	{
		private readonly IUser _userRepository;
		private readonly IMapper _mapper;

		public AuthService(IUser userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}
		public async Task<UserDto?> Authenticate(string email, string password)
		{
			var user = await _userRepository.GetUserByEmailAsync(email);
			if (user is null || !user.PasswordHash.VerifyPassword(password))
				throw new UnauthorizedAccessException("کاربری با این مشخصات یافت نشد");
			return _mapper.Map<UserDto>(user);
		}
	}
}
