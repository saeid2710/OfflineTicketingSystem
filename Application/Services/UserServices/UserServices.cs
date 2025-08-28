
using AutoMapper;
using Contracts.UserContract;
using Entities.Users;
using Shared.Dto;
using Shared.Utils;

namespace Application.Services.UserServices
{
	public class UserServices
	{
		private readonly IUser _userRepository;
		private readonly IMapper _mapper;

		public UserServices(IUser userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}


		public async Task<Guid?> CreateUser(CreateUserDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			return await _userRepository.AddUserAsync(user);
		}

		public async Task<Guid?> UpdateUser(CreateUserDto userDto, Guid userId)
		{
			var user = await _userRepository.GetUserByIdAsync(userId);
			if (user is null) throw new Exception("کاربر پیدا نشد");

			user.Email=userDto.Email;
			user.FullName=userDto.FullName;
			user.Role=userDto.Role;
			user.PasswordHash = userDto.PasswordHash.HashPassword();
			return await _userRepository.UpdateUserAsync(user);

		}

		public async Task<UserDto> GetUserByEmail(string email)
		{
			var user = await _userRepository.GetUserByEmailAsync(email);
			if (user is null) throw new Exception("کاربری پیدا نشد");
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserById(Guid userId)
		{
			var user = await _userRepository.GetUserByIdAsync(userId);
			if (user is null) throw new Exception("کاربری پیدا نشد");
			return _mapper.Map<UserDto>(user);	
		}

		public async Task<List<UserDto>> GetAllUser()
		{
		var users= await _userRepository.GetAllUserAsync();
		return _mapper.Map<List<UserDto>>(users);
		}

		public async Task<bool> RemoveUser(Guid userId)
		{
			var user = await _userRepository.GetUserByIdAsync(userId);
			if (user is null) throw new Exception("کاربری پیدا نشد");
			return await _userRepository.RemoveUserAsync(user);
		}
	}
}
