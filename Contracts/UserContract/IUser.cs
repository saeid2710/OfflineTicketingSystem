using Entities.Users;

namespace Contracts.UserContract;

public interface IUser
{
	Task<User?> GetUserByIdAsync(Guid userId);
	Task<User?> GetUserByEmailAsync(string email);
	Task<IEnumerable<User>> GetAllUserAsync();
	Task<Guid> AddUserAsync(User user);
	Task<bool> RemoveUserAsync(User user);
	Task<Guid> UpdateUserAsync(User user);
}